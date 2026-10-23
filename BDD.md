# Exo 1 BDD

![[MCD Exo1.png]]
Blessure(<u>ID</u>, nomBlessure)
Grade(<u>ID</u>, libelleGrade)
Unité(<u>ID</u>,nomUnité)
Soldat(<u>ID</u>,nom,prenom,dateNaissance,dateDeces)
Bataille(<u>ID</u>,nom,lieu,dateDebut,dateFin)

Affecté(<u>ID.unité</u>, <u>ID.soldat</u>, dateIntegration, dateDepart)
Obtenir(<u>ID.soldat</u>, <u>ID.grade</u>, dateObtention)
Contraction(<u>ID.soldat</u>, <u>ID.blessure</u>, <u>ID.bataille</u>, dateBlessure, estMortel)


# Exo 2 BDD

## Etape 1
![[Pasted image 20260924110847.png]]


## Etape 2
![[Pasted image 20260924105853.png]]
![[Pasted image 20260924120347.png]]


CIF => Contrainte d'intégrité fonctionnel 
	0,**1** ou 1,**1** => 0,**N** ou 1,**N**
	Clé primaire N qui transitionne vers 1 en clé étrangère

CIM => Contrainte d'intégrité multiple
	0,**N** ou 1,**N** => 0,**N** ou 1,**N**

# Algèbre Relationnel (H.S.)

PERSONNES = (<u>ID</u>, nom, prenom, tel, ville, #idService, #idPoste)
SERVICE = (<u>idService</u>, nom)
POSTE = (<u>ID</u>, nomPoste)
![[Drawing 2026-09-24 12.33.24.excalidraw]]

? Nom, Prenom, Tel des membres du service "comptabilité"
![[Drawing 2026-09-24 12.42.23.excalidraw]]

? Nom, Prenom, Nom de Service, Nom de Poste de toutes les personnes
![[Drawing 2026-09-24 12.48.14.excalidraw]]

? Nom, Prenom, Service de tous les "chef de projet"
![[Drawing 2026-09-24 14.09.57.excalidraw]]

# Exo Algèbre Relationnelle :
![[Drawing 2026-10-08 11.07.07.excalidraw]]


# TD Algèbre Relationnelle

ETUDIANT(<u>idEtudiant</u>, nom, prenom, ville)
ENSEIGNANT(<u>idEnseignant</u>, nom, prenom)
MATIERE(<u>idMatiere</u>, libelle)
COURS(<u>idCours</u>, #idMatiere, #idEnseignant, anneeUniversitaire)
INSCRIPTION(<u>#idEtudiant</u>, <u>#idCours</u>, note)

## Niveau 1 — Un seul opérateur
![[Drawing 2026-10-15 10.00.01.excalidraw]]

## Niveau 2 — Projection et restriction
![[Drawing 2026-10-15 10.00.02.excalidraw]]

## Niveau 3 — Une jointure
![[Drawing 2026-10-15 10.00.03.excalidraw]]

## Niveau 4 — Plusieurs jointures et conditions
![[Drawing 2026-10-15 10.00.04.excalidraw]]


---

# TD MySQL – Triggers et procédures stockées (tp_stock)

> Exécuté sur MySQL 8.0.44 (root). Les 3 triggers ont été créés avant les tests : la mise à jour du prix de l'exercice 2 (25 → 27) est donc déjà historisée (ligne 1 de `historique_prix`), ce qui explique les 2 lignes visibles à l'exercice 3.

## Installation – questions
- **Clés** : `produit.id_produit` et `historique_prix.id_historique` sont des clés primaires (AUTO_INCREMENT). `historique_prix.id_produit` est une clé étrangère vers `produit`.
- **Clé étrangère** : garantit qu'une ligne d'historique référence un produit existant (intégrité référentielle) ; empêche aussi de supprimer un produit encore historisé.
- **DECIMAL pour un prix** : valeur exacte en base 10 ; FLOAT/DOUBLE sont approximatifs et produisent des erreurs d'arrondi (0.1 + 0.2 ≠ 0.3).
- **0 vs NULL** : 0 est une valeur connue (stock épuisé) ; NULL signifie « inconnu / absent ». `NULL = 0` n'est ni vrai ni faux, il faut `IS NULL`.

## Exercice 1 – Trigger BEFORE INSERT
```sql
DELIMITER $$
CREATE TRIGGER trg_produit_bi_controle
BEFORE INSERT ON produit
FOR EACH ROW
BEGIN
  IF NEW.prix <= 0 THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Le prix doit etre strictement positif';
  END IF;
  IF NEW.stock < 0 THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Le stock ne peut pas etre negatif';
  END IF;
END$$
DELIMITER ;
```
Résultats observés :
- `('Cle USB 64 Go', 12.90, 30)` → OK (id 6)
- `('Produit incorrect A', -5.00, 10)` → `ERROR 1644 (45000): Le prix doit etre strictement positif`
- `('Produit incorrect B', 15.00, -3)` → `ERROR 1644 (45000): Le stock ne peut pas etre negatif`
- `('Produit gratuit', 0, 5)` → `ERROR 1644 (45000): Le prix doit etre strictement positif`

**Réponse** : les produits refusés ne sont pas présents (la table contient les 5 produits d'origine + la clé USB). Le contrôle est déclenché par l'événement `INSERT`, avant l'écriture (BEFORE) : le `SIGNAL` annule l'instruction.

## Exercice 2 – Trigger BEFORE UPDATE
```sql
DELIMITER $$
CREATE TRIGGER trg_produit_bu_controle
BEFORE UPDATE ON produit
FOR EACH ROW
BEGIN
  IF NEW.prix <= 0 THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Le prix doit etre strictement positif';
  END IF;
  IF NEW.stock < 0 THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Le stock ne peut pas etre negatif';
  END IF;
END$$
DELIMITER ;
```
Résultats : `prix = 27.00` → OK ; `stock = -1` → erreur « Le stock ne peut pas etre negatif » ; `prix = 0` → erreur « Le prix doit etre strictement positif ». État final du produit 1 : **prix 27.00, stock 20** ✔.

**Réponse** : un trigger `BEFORE INSERT` ne se déclenche que sur les insertions ; un `UPDATE` pourrait donc introduire un prix ou un stock invalide après coup. Il faut un second trigger sur `UPDATE`.

## Exercice 3 – Trigger AFTER UPDATE (historique)
```sql
DELIMITER $$
CREATE TRIGGER trg_produit_au_historique
AFTER UPDATE ON produit
FOR EACH ROW
BEGIN
  IF NEW.prix <> OLD.prix THEN
    INSERT INTO historique_prix (id_produit, ancien_prix, nouveau_prix)
    VALUES (OLD.id_produit, OLD.prix, NEW.prix);
  END IF;
END$$
DELIMITER ;
```
Résultats :
- `prix = 29.90` → nouvelle ligne `(2, produit 1, 27.00 → 29.90)`
- `stock = stock + 5` → aucune ligne ajoutée (stock 25)
- `prix = prix` → aucune ligne ajoutée

| id_historique | id_produit | ancien_prix | nouveau_prix |
|---|---|---|---|
| 1 | 1 | 25.00 | 27.00 |
| 2 | 1 | 27.00 | 29.90 |

**Réponses** :
- Comparer `OLD.prix` et `NEW.prix` évite d'historiser quand le prix n'a pas réellement changé (modif du stock seul, ou `prix = prix`).
- `AFTER` : on n'historise que si la modification a été acceptée (les contrôles BEFORE et les contraintes sont passés) ; ainsi aucune trace d'une modification refusée.
- Plusieurs lignes modifiées : un trigger est `FOR EACH ROW`, il s'exécute une fois par ligne modifiée → une ligne d'historique par produit dont le prix change.

## Exercice 4 – Procédure avec paramètre IN
```sql
DELIMITER $$
CREATE PROCEDURE sp_produits_stock_faible(IN p_seuil INT)
BEGIN
  IF p_seuil IS NULL OR p_seuil < 0 THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Le seuil doit etre un entier positif ou nul';
  END IF;
  SELECT id_produit, designation, prix, stock
  FROM produit
  WHERE stock <= p_seuil
  ORDER BY stock, id_produit;
END$$
DELIMITER ;
```
Résultats : `CALL …(10)` → Webcam HD (stock 0), Ecran 24 pouces (stock 10) ; `CALL …(0)` → Webcam HD ; `(-1)` et `(NULL)` → erreur « Le seuil doit etre un entier positif ou nul ».

**Réponse** : `p_seuil` est le paramètre reçu de l'appelant (valeur de comparaison, propre à la procédure) ; `stock` est une colonne de la table, qui varie par ligne. (Il faut éviter de nommer un paramètre comme une colonne, d'où le préfixe `p_`.) Le produit au stock nul initial est **Webcam HD (id 5)**.

## Exercice 5 – Réapprovisionner
```sql
DELIMITER $$
CREATE PROCEDURE sp_reapprovisionner(IN p_id_produit INT, IN p_quantite INT)
BEGIN
  DECLARE v_existe INT DEFAULT 0;
  IF p_quantite IS NULL OR p_quantite <= 0 THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'La quantite doit etre strictement positive';
  END IF;
  SELECT COUNT(*) INTO v_existe FROM produit WHERE id_produit = p_id_produit;
  IF v_existe = 0 THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Produit inexistant';
  END IF;
  UPDATE produit SET stock = stock + p_quantite WHERE id_produit = p_id_produit;
  SELECT id_produit, designation, stock FROM produit WHERE id_produit = p_id_produit;
END$$
DELIMITER ;
```
Résultats : `(5, 8)` → Webcam HD, **stock = 8** ; `(999, 5)` → « Produit inexistant » ; `(1, -2)` et `(1, NULL)` → « La quantite doit etre strictement positive ».

**Réponses** :
- Existence : `SELECT COUNT(*) INTO v_existe … WHERE id_produit = p_id_produit` puis test `v_existe = 0`.
- Tester `IS NULL` : `NULL <= 0` vaut NULL (ni vrai ni faux), donc le `IF` ne se déclencherait pas et `stock + NULL` mettrait le stock à NULL (rejeté par NOT NULL, mais avec une erreur peu explicite).
- Les appels refusés n'ont modifié aucun stock (les `SIGNAL` interviennent avant l'`UPDATE`) : seul le produit 5 a changé.

## Exercice 6 – Paramètre OUT
```sql
DELIMITER $$
CREATE PROCEDURE sp_valeur_stock(IN p_id_produit INT, OUT p_valeur DECIMAL(12,2))
BEGIN
  DECLARE v_existe INT DEFAULT 0;
  SELECT COUNT(*) INTO v_existe FROM produit WHERE id_produit = p_id_produit;
  IF v_existe = 0 THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Produit inexistant';
  END IF;
  SELECT prix * stock INTO p_valeur FROM produit WHERE id_produit = p_id_produit;
END$$
DELIMITER ;
```
Résultats : `CALL sp_valeur_stock(5, @valeur); SELECT @valeur;` → **319.20** (39.90 × 8) ; `(999, @valeur)` → « Produit inexistant ».

**Réponses** :
- `OUT` : renvoie une valeur à l'appelant (la procédure l'écrit, la valeur initiale n'est pas lue).
- `@valeur` : variable de session qui reçoit la sortie et reste lisible après le `CALL` (`SELECT @valeur`).
- Une variable `DECLARE` est locale au bloc `BEGIN…END` : elle disparaît à la fin de la procédure, inaccessible après `CALL`.

## Exercice 7 – Vendre un produit
```sql
DELIMITER $$
CREATE PROCEDURE sp_vendre(IN p_id_produit INT, IN p_quantite INT)
BEGIN
  DECLARE v_existe INT DEFAULT 0;
  IF p_quantite IS NULL OR p_quantite <= 0 THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'La quantite doit etre strictement positive';
  END IF;
  SELECT COUNT(*) INTO v_existe FROM produit WHERE id_produit = p_id_produit;
  IF v_existe = 0 THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Produit inexistant';
  END IF;
  UPDATE produit SET stock = stock - p_quantite
  WHERE id_produit = p_id_produit AND stock >= p_quantite;
  IF ROW_COUNT() = 0 THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Stock insuffisant';
  END IF;
  SELECT id_produit, designation, stock AS stock_restant FROM produit WHERE id_produit = p_id_produit;
END$$
DELIMITER ;
```
Résultats : `(5, 3)` → **stock restant 5** ; `(5, 50)` → « Stock insuffisant » (stock reste 5) ; `(999, 1)` → « Produit inexistant » ; `(5, 0)` → « La quantite doit etre strictement positive ». Vérification finale : produit 5 = stock 5, `historique_prix` toujours à 2 lignes.

**Réponses** :
- Triggers exécutés : `trg_produit_bu_controle` (BEFORE UPDATE) puis `trg_produit_au_historique` (AFTER UPDATE), car la vente est un `UPDATE`.
- Aucune ligne d'historique : le prix ne change pas (`OLD.prix = NEW.prix`), seul le stock varie.
- Risque d'un `SELECT` du stock suivi d'un `UPDATE` indépendant : entre les deux, une autre session peut vendre le même produit (condition de concurrence / « lost update ») → stock négatif ou survente. Le `UPDATE … WHERE stock >= p_quantite` rend le test et la modification atomiques.

## Bilan – objets créés
| Objet | Type | Rôle |
|---|---|---|
| `trg_produit_bi_controle` | trigger BEFORE INSERT | refuse prix ≤ 0 et stock < 0 à l'insertion |
| `trg_produit_bu_controle` | trigger BEFORE UPDATE | mêmes règles à la modification |
| `trg_produit_au_historique` | trigger AFTER UPDATE | historise les changements de prix |
| `sp_produits_stock_faible` | procédure (IN) | liste les produits sous un seuil de stock |
| `sp_reapprovisionner` | procédure (IN) | augmente le stock d'un produit |
| `sp_valeur_stock` | procédure (IN/OUT) | renvoie prix × stock |
| `sp_vendre` | procédure (IN) | diminue le stock si suffisant, sinon erreur |

`SHOW TRIGGERS` et `SHOW PROCEDURE STATUS` confirment les 3 triggers et les 4 procédures dans `tp_stock`.

## Récap express – réponses à dire à l'oral
- **Clés ?** PK : `id_produit`, `id_historique`. FK : `historique_prix.id_produit` → `produit`.
- **Rôle de la FK ?** Garantit qu'un historique pointe vers un produit qui existe (intégrité référentielle).
- **Pourquoi DECIMAL ?** Valeur exacte, pas d'erreur d'arrondi comme FLOAT.
- **0 vs NULL ?** 0 = valeur connue ; NULL = inconnu, se teste avec `IS NULL`.
- **Ex1 – Refusés présents ? Événement ?** Non, absents. Le contrôle se déclenche sur `INSERT`, avant l'écriture (BEFORE) : le `SIGNAL` annule l'insertion.
- **Ex2 – Pourquoi le trigger de l'ex1 ne suffit pas ?** Il ne couvre que `INSERT` ; un `UPDATE` pourrait mettre un prix ou stock invalide. Il faut un trigger `BEFORE UPDATE`.
- **Ex3 – Pourquoi comparer OLD/NEW ?** Pour n'historiser que si le prix change vraiment.
- **Ex3 – Pourquoi AFTER ?** On historise seulement une modification acceptée (contrôles passés).
- **Ex3 – Plusieurs lignes modifiées ?** Le trigger est `FOR EACH ROW` : une ligne d'historique par produit modifié.
- **Ex4 – `p_seuil` vs `stock` ?** `p_seuil` = paramètre reçu de l'appelant ; `stock` = colonne de la table, différente à chaque ligne. Stock nul initial : Webcam HD (id 5).
- **Ex5 – Vérifier l'existence ?** `SELECT COUNT(*) INTO v_existe … WHERE id_produit = p_id_produit`, puis `v_existe = 0` → erreur.
- **Ex5 – Pourquoi `IS NULL` ?** `NULL <= 0` n'est ni vrai ni faux : le test passerait à côté du NULL.
- **Ex5 – Appels refusés ont changé les stocks ?** Non, les `SIGNAL` arrivent avant l'`UPDATE`.
- **Ex6 – Rôle de OUT ?** Renvoie une valeur à l'appelant.
- **Ex6 – Pourquoi `@valeur` ?** Variable de session qui reçoit la sortie et reste lisible après le `CALL`.
- **Ex6 – Variable `DECLARE` après CALL ?** Non, elle est locale au bloc `BEGIN…END` et disparaît à la fin.
- **Ex7 – Quels triggers ?** `trg_produit_bu_controle` (BEFORE UPDATE) et `trg_produit_au_historique` (AFTER UPDATE).
- **Ex7 – Pourquoi aucune ligne d'historique ?** Le prix ne change pas, seul le stock varie.
- **Ex7 – Risque lecture puis modification ?** Concurrence : une autre session peut vendre entre les deux → survente / stock négatif. Un seul `UPDATE … WHERE stock >= qte` + `ROW_COUNT()` règle ça (atomique).
