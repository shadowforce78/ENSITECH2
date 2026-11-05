# Easy2Drive – Dossier A : Sécurisation de l'application e-learning

BTS SIO SLAM – U6 Cybersécurité des services informatiques – Session 2022

## Mission A1 : Évaluation des risques (DICP)

### A.1.1 a) Disponibilité : récit 1 (`**`) contre récit 2 (`*`)

- **Récit 1** (consultation du site sans compte) : c'est la vitrine commerciale d'Easy2Drive. Si le site est indisponible, l'entreprise perd des prospects (familles, auto-écoles) et de l'image. Le besoin est donc **important**.
- **Récit 2** (modification du mot de passe) : la fonction n'est pas urgente. L'élève peut réessayer plus tard sans conséquence grave. Le besoin est **modéré**.

### A.1.1 b) Récit 2 : intégrité `**` et confidentialité `**`

- **Intégrité** : le nouveau mot de passe doit être enregistré exactement, sans altération ni troncature. Sinon l'élève ne peut plus se connecter, ou un tiers pourrait imposer un mot de passe.
- **Confidentialité** : le mot de passe est un secret. S'il est divulgué, un tiers peut usurper l'identité de l'élève et accéder à ses données personnelles (NEPH, résultats, messages).

### A.1.1 c) Preuve : récit 1 (`-`) contre récit 3 (`**`)

- **Récit 1** : consultation anonyme, sans engagement ni donnée échangée. Aucune contestation n'est possible, donc aucune trace n'est nécessaire.
- **Récit 3** : l'avis publié sur une auto-école peut être diffamatoire ou faux, et l'auto-école peut le contester. Il faut pouvoir prouver qui l'a écrit, quand et avec quel contenu (traces opposables).

## Mission A2 : RGPD

### A.2.1 Cookies

Le bandeau actuel (A2) dit seulement "nous utilisons des cookies pour nous assurer du bon fonctionnement de notre site". Il ne respecte pas la délibération CNIL du 17 septembre 2020 :

- les **finalités** des cookies ne sont pas clairement détaillées ;
- **aucun choix** n'est proposé (accepter, refuser, gérer) ;
- le **consentement est implicite** (poursuite de la navigation), alors qu'il doit être explicite ;
- depuis le 1er avril 2021, l'entreprise s'expose à une **amende**.

### A.2.2 Données à caractère personnel du récit 1

La famille consulte le site sans compte, donc seules les données collectées automatiquement (A3) sont concernées.

- La donnée à caractère personnel est l'**adresse IP** : elle permet d'identifier indirectement une personne physique (via le fournisseur d'accès).
- Le type d'appareil, le navigateur, la résolution d'écran, l'OS et la langue ne sont pas personnels pris isolément. Leur **combinaison** peut permettre de recouper et d'identifier quelqu'un (empreinte du navigateur), donc ils peuvent être considérés comme personnels par recoupement.

## Mission A3 : Sécurité du mot de passe

### A.3.1 a) Ce qui ne va pas dans le mot de passe initial

- Il est envoyé **en clair par courriel** (A4). Il est lisible dans la boîte mail et sur le trajet, avec l'identifiant dans le même message.
- Il est généré à l'avance, donc connu de l'auto-école et de la plateforme.
- L'élève peut le **garder un an** sans renouvellement.
- Il est faible : 8 caractères, alors que la CNIL en demande 12.

### A.3.1 b) Meilleure solution

- Envoyer un **lien d'activation à usage unique et à durée limitée**. L'élève y choisit lui-même son mot de passe, qui respecte la politique CNIL.
- Autre option : un mot de passe temporaire à changer obligatoirement à la première connexion, ou un identifiant par courriel et le mot de passe par un autre canal (SMS).

### A.3.2 a) Politique actuelle de `verifPassword`

- Longueur d'au moins 8 caractères (`points_long` vaut 1, sinon 0).
- Complexité : minuscule (1) + majuscule (2) + chiffre (3) = 6 points.
- Le résultat est `points_long * points_comp`, et il doit être égal à 6.
- Le mot de passe doit donc contenir au moins 8 caractères, avec une minuscule, une majuscule et un chiffre. Le caractère spécial n'est pas exigé.

### A.3.2 b) Modification de `verifPassword`

La CNIL demande au moins 12 caractères et les 4 critères (1+2+3+4 = 10 points). Seules les lignes modifiées ou ajoutées sont données.

```php
1   $points_total = 10;
5   if ($longueur >= 12) { $points_long = 1; }
    // ajout après la ligne 8 :
8b  if (preg_match("/\W/", $mdp)) { $points_comp = $points_comp + 4; }
10  return ($resultat >= $points_total);
```

### A.3.2 c) Tests unitaires

L'ancien test `"qamQdVD3"` (valide jusqu'ici) doit maintenant renvoyer `false` : il fait 8 caractères et n'a pas de caractère spécial.

```php
public function testVerifPassword()
{
    $this->assertSame(false, verifPassword("Qam3"));          // trop court
    $this->assertSame(false, verifPassword("qamQdVD3"));      // 8 car., sans spécial
    $this->assertSame(false, verifPassword("qamQdVDbdAbc"));  // 12 car., sans chiffre ni spécial
    $this->assertSame(false, verifPassword("qamqdvdbabc3"));  // sans majuscule ni spécial
    $this->assertSame(false, verifPassword("QAMQDVDBABC3"));  // sans minuscule ni spécial
    $this->assertSame(false, verifPassword("QAMQDVD3BDA!"));  // sans minuscule
    $this->assertSame(false, verifPassword("qamQdVD3bdAb"));  // 12 car., sans spécial
    $this->assertSame(false, verifPassword("qamQdV3!bdA"));   // 4 critères mais 11 car.
    $this->assertSame(true,  verifPassword("qamQdVD3bdA!"));  // 12 car., 4 critères
}
```

### A.3.3 a) Ajout du champ `dateMajMDP`

```sql
ALTER TABLE Utilisateur ADD dateMajMDP DATE NOT NULL DEFAULT (CURRENT_DATE());
```

`DEFAULT (CURRENT_DATE())` demande MySQL 8.0.13 ou plus récent. Sur une version plus ancienne, il faut un trigger `BEFORE INSERT`.

### A.3.3 b) Fonction stockée `renouvelleMDP`

```sql
CREATE FUNCTION renouvelleMDP(num INT)
RETURNS BOOLEAN
BEGIN
    DECLARE v_date DATE;
    DECLARE v_retour BOOLEAN DEFAULT false;
    SELECT dateMajMDP INTO v_date FROM Utilisateur WHERE id = num;
    IF (DATE_ADD(v_date, INTERVAL 90 DAY) < CURRENT_DATE()) THEN
        SET v_retour = true;
    END IF;
    RETURN v_retour;
END
```

À chaque changement de mot de passe, il faudra aussi mettre `dateMajMDP` à la date du jour.
