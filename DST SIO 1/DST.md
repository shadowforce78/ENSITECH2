DST SIO2 SLAM

Dossier 1 : MCD "apports de raisin"

![[MCD.excalidraw]] 

Dossier 2 : gestion des événements redoutés

1 - Le site web ne répond pas.
Impact : les abonnés n'ont plus accès au service, risque de perte d'abonnés et de chiffre d'affaires.
Gravité : *

2 - Un attaquant accède à la base de données et modifie les articles.
Impact : de fausses informations sont diffusées aux abonnés, perte de confiance et mauvaise image de l'entreprise, risque juridique.
Gravité : **

Besoins en sécurité (D / I / C / P)

1 - PDF : ** / ** / * / -

2 - Commentaires publics : * / * / - / **
Pas de confidentialité car les commentaires sont publics. Il faut pouvoir retrouver l'auteur en cas de problème.

3 - Mot de passe : * / ** / ** / *
Le mot de passe doit rester secret et ne doit être modifiable que par son propriétaire.

Dossier 3

Exercice 1

Le tableau jours contient 7 éléments (indices 0 à 6) mais la boucle s'arrête à i < 50. Quand i vaut 7, jours[7] n'existe pas : le programme plante avec une IndexOutOfRangeException.
Correction : while (i < jours.Length)

Exercice 2

Usage : la fonction GeneratePassword génère un mot de passe aléatoire de la longueur donnée. Chaque caractère est tiré au hasard dans la chaîne chars (lettres majuscules et minuscules).

Exercice 3

La fonction Numero_Du_Jour utilise un switch et renvoie -1 par défaut (jour inconnu).

![[code.png]]