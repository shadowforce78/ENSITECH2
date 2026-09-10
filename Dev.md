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