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
