# Easy2Drive – Dossier B : Prise en compte des conclusions de l'audit de sécurité

BTS SIO SLAM – U6 Cybersécurité des services informatiques – Session 2022

## Mission B1 : Attribution de la Garantie Réussite

### B.1.1 Conséquence d'une déclaration abusive d'échec

La conséquence principale est **financière**. Easy2Drive rembourse à tort les frais de présentation à l'examen à un élève qui ne remplit pas les conditions (ou qui n'a pas échoué). S'y ajoutent une atteinte à l'image et une perte de confiance dans la garantie.

### B.1.2 a) Analyse du déclencheur `check_garantie_reussite`

Comparaison des conditions du document B1 avec le code du document B2 :

| Condition (B1) | Dans le trigger | Verdict |
|---|---|---|
| Échec de moins de 6 mois | Ligne 12 : `DATE_ADD(NEW.dateEtg, INTERVAL 6 MONTH) >= NOW()` lève l'erreur « échec trop ancien » | **Faux** : la comparaison est inversée. Elle bloque les échecs récents et laisse passer les échecs de plus de 6 mois. Il faut `< NOW()`. |
| Au moins 25 séries de quiz | Ligne 16 : `COUNT(*)` sur `Evaluer` avec `WHERE idEleve = NEW.id` | **Mal implémenté** : `Eleve` n'a pas de colonne `id`, la clé est `idUtilisateur`. Il faut `NEW.idUtilisateur`. Variante possible : `COUNT(DISTINCT idSerieTest)`, car une même série peut être passée plusieurs fois (la clé de `Evaluer` contient `dateHeure`). |
| Au moins 4 examens blancs | Aucune vérification du nombre d'examens | **Absent**. De plus, avec 0 examen, `AVG` renvoie `NULL`, la comparaison `NULL < 34` n'est pas vraie et la garantie est accordée. Avec 1 à 3 examens, la moyenne porte sur moins de 4 notes. Le code d'erreur `10004` est d'ailleurs le seul manquant dans la série des `SIGNAL`. |
| Moyenne d'au moins 34/40 sur les 4 meilleurs examens | Ligne 21 : `WHERE idEleve = NEW.id` | **Mal implémenté** : même erreur `NEW.id` à remplacer par `NEW.idUtilisateur`. |
| Garantie une seule fois après le premier échec | Ligne 7 : erreur si `OLD.echecEtg = TRUE AND NEW.echecEtg = TRUE` | **Globalement correct** pour le second échec |
| Attribution effective | Seulement le commentaire « Garantie Réussite attribuée » | **Absent** : rien n'affecte `garantieReussite`. |

Remarque : le trigger est `BEFORE UPDATE ON Eleve`, donc il s'exécute à toute modification d'un élève (par exemple un changement d'adresse). Il serait plus sûr de ne contrôler que le passage de `echecETG` à vrai. Je ne l'ai pas intégré à la correction ci-dessous pour rester dans le périmètre de la question.

### B.1.2 b) Code corrigé (parties à modifier ou à ajouter)

```sql
-- ajout dans les déclarations
DECLARE v_nbExam INT;

-- ligne 12 : comparaison inversée
IF DATE_ADD(NEW.dateEtg, INTERVAL 6 MONTH) < NOW() THEN

-- ligne 16 : clé de Eleve (et séries distinctes, au choix)
SELECT COUNT(DISTINCT idSerieTest) INTO v_nbSerie FROM Evaluer
WHERE idEleve = NEW.idUtilisateur;

-- ajout après la ligne 20 : contrôle du nombre d'examens blancs
SELECT COUNT(*) INTO v_nbExam FROM Passer WHERE idEleve = NEW.idUtilisateur;
IF v_nbExam < 4 THEN
    SIGNAL SQLSTATE '10004'
    SET MESSAGE_TEXT = 'Garantie réussite : nombre d''examens blancs insuffisant';
END IF;

-- ligne 21 : clé de Eleve
SELECT AVG(examenScore) INTO v_scoreMoyen FROM (SELECT examenScore FROM Passer
WHERE idEleve = NEW.idUtilisateur ORDER BY examenScore DESC LIMIT 4) AS MeilleureNotes;

-- à la fin, à la place du commentaire : attribution effective
SET NEW.garantieReussite = TRUE;
```

## Mission B2 : Traçage des événements sur les données personnelles

### B.2.1 Schéma de la base `BD_RGPD_LOGS`

Le schéma à compléter du document B5 est une image, que je n'ai pas pu lire. Voici donc un modèle complet à reporter dans votre formalisme (diagramme de classes ou schéma entité-association).

Besoin (document B3) : savoir **quel utilisateur, sous quel rôle, a fait quelle action (consultation, insertion, modification, suppression) sur quel enregistrement de quelle table, et quand**, avec une recherche possible par auto-école.

**Schéma relationnel proposé :**

```
Role (id, libelle)                          -- directeur, formateur, élève, modérateur
Action (id, libelle)                        -- consultation, insertion, modification, suppression
TableDonnees (id, nom)                      -- Eleve, Formateur, Message, ...
UtilisateurLog (idUtilisateur, nom, prenom, idAutoEcole, idRole)
Evenement (id, dateHeure, idUtilisateur, idAction, idTable, idEnregistrement)

Evenement.idUtilisateur  -> UtilisateurLog.idUtilisateur
Evenement.idAction       -> Action.id
Evenement.idTable        -> TableDonnees.id
UtilisateurLog.idRole    -> Role.id
```

Remarques de conception :
- `BD_RGPD_LOGS` est sur un autre serveur, donc **aucune clé étrangère vers `BD_EASY`**. L'identifiant utilisateur et l'identifiant de l'enregistrement sont de simples valeurs conservées. `nom`, `prenom`, `idAutoEcole` et `idRole` sont recopiés dans `UtilisateurLog`, ce qui permet de répondre sans accéder à `BD_EASY`.
- `idEnregistrement` désigne l'ID de la ligne concernée dans la table `idTable` (ex. 1025 dans `Formateur`).
- Cas « Samira Ghalam poste le message n°485685 » : un `Evenement` avec l'action *insertion*, la table `Message` et l'enregistrement 485685.
- Cas « Fabien Leroux consulte les élèves 2348 et 1685 » : deux `Evenement`, un par enregistrement consulté.

### B.2.2 Création de l'utilisateur

La base est sur un serveur distinct, donc l'utilisateur doit être autorisé à se connecter depuis le serveur Web `Easy2Drive.fr`, et non depuis `localhost`.

```sql
CREATE USER 'APPLI_RGPD_LOGS'@'Easy2Drive.fr' IDENTIFIED BY 'unMotDePasseRobuste12+';
```

Le mot de passe doit lui-même respecter la politique CNIL (12 caractères ou plus, 4 types de caractères).

### B.2.3 Droit d'ajout uniquement

```sql
GRANT INSERT ON BD_RGPD_LOGS.* TO 'APPLI_RGPD_LOGS'@'Easy2Drive.fr';
```

Seule l'insertion est autorisée. L'application ne peut ni lire, ni modifier, ni supprimer. Les journaux ne sont donc pas altérables, comme l'exige la CNIL (document CNIL 3). La lecture est faite par un autre compte en lecture seule, utilisé par l'application « Logs RGPD ».

### B.2.4 Durée de conservation et purge

- **a) Durée :** **6 mois** maximum, en période glissante. C'est la limite que la CNIL fixe pour les journaux, sauf obligation légale ou risque particulièrement important (document CNIL 3).
- **b) Document RGPD :** le **registre des traitements** (article 30 du RGPD), où figurent les durées de conservation. Cette durée doit aussi apparaître dans la politique de confidentialité pour informer les personnes.
- **c) Purge automatique :** un **événement planifié MySQL** (`EVENT`) qui s'exécute chaque jour et supprime les lignes de plus de 6 mois :

```sql
CREATE EVENT purge_evenements
ON SCHEDULE EVERY 1 DAY
DO
    DELETE FROM Evenement WHERE dateHeure < DATE_SUB(NOW(), INTERVAL 6 MONTH);
```

Il faut activer l'ordonnanceur (`SET GLOBAL event_scheduler = ON;`). L'événement s'exécute avec les droits de son créateur (un compte d'administration), pas avec `APPLI_RGPD_LOGS`, qui n'a que le droit INSERT. Autre solution : une tâche `cron` qui lance le même `DELETE` chaque nuit.
