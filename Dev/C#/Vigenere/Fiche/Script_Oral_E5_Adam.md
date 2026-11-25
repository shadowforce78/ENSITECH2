# Script Oral E5 — Adam Planque — BTS SIO SLAM
## ~9 min 30 d'exposé · Portfolio ouvert en support

---

## INTRODUCTION (0:00 → 0:45)

Bonjour,

Je suis Adam Planque, étudiant en deuxième année de BTS SIO option SLAM au centre de formation ENSITECH. Je suis en alternance depuis deux ans à l'École Nationale Supérieure de Paysage de Versailles, au sein de la Direction des Systèmes d'Information.

Aujourd'hui, je vais vous présenter deux réalisations professionnelles issues de mon alternance qui illustrent les compétences du bloc 1. La première concerne le déploiement d'un système d'affichage dynamique avec Xibo, et la seconde, la mise en place de l'authentification à deux facteurs sur notre serveur CAS.

---

## PARCOURS (0:45 → 2:00)

> *[Montrer la page d'accueil du portfolio, section "À propos" / "Parcours"]*

La DSI de l'ENSP, c'est une petite équipe. On gère l'ensemble du système d'information de l'école : le réseau, le parc informatique, les serveurs, et le support aux utilisateurs — aussi bien les enseignants que le personnel administratif et les étudiants.

En deux ans d'alternance, j'ai touché à beaucoup de choses : de l'administration réseau, du développement, de la gestion de tickets, de la maintenance matérielle. Mes responsabilités ont évolué : au départ j'assistais mon collègue sur les tâches courantes, et aujourd'hui je gère des projets de bout en bout, de l'étude du besoin jusqu'à la formation des utilisateurs.

Les deux réalisations que je vais vous présenter sont représentatives de cette montée en compétences.

---

## RÉALISATION 1 — XIBO : AFFICHAGE DYNAMIQUE (2:00 → 5:30)

### Le besoin

Mon responsable à la DSI m'a sollicité parce que l'école utilisait de vieux diaporamas PowerPoint affichés en boucle sur des écrans dans les couloirs. C'était rigide : chaque modification nécessitait d'aller physiquement brancher une clé USB sur chaque écran. Le service communication souhaitait pouvoir mettre à jour les contenus à distance et de manière autonome, sans passer par la DSI à chaque fois.

Le besoin était donc clair : mettre en place un système d'affichage dynamique centralisé, que le service communication puisse piloter lui-même.

### Mon rôle

J'ai pris en charge le projet seul, de A à Z. Mon responsable m'a donné la direction, mais c'est moi qui ai fait l'étude des solutions, l'installation, la configuration et la formation.

### Ma démarche

J'ai d'abord fait des recherches sur les solutions open source d'affichage dynamique. J'ai retenu **Xibo** parce qu'il est gratuit, bien documenté, et qu'il propose une interface web intuitive — ce qui était essentiel puisque les utilisateurs finaux n'ont pas de profil technique.

Côté technique, j'ai déployé Xibo en **conteneurs Docker sur un serveur Linux**. Ça m'a permis d'avoir une installation propre, facile à maintenir et à sauvegarder. J'ai configuré le serveur CMS Xibo, puis j'ai installé le client player sur les écrans.

Une difficulté que j'ai rencontrée : la communication entre le serveur et les players passait par le réseau de l'école, et il a fallu vérifier que les flux étaient correctement ouverts, notamment pour la synchronisation des contenus. J'ai fait des tests avec un premier écran avant de déployer sur les trois.

### Le résultat

> *[Montrer éventuellement une capture de Xibo ou la section projets du portfolio]*

Aujourd'hui, le système fonctionne sur **3 écrans**, avec d'autres prévus. Le service communication gère ses contenus de manière totalement autonome.

Pour assurer cette autonomie, j'ai rédigé une **documentation complète sous Word** : guide d'utilisation pas à pas avec captures d'écran, et je suis allé présenter l'outil en direct au service communication pour les former.

### Compétences mobilisées

Cette réalisation mobilise plusieurs compétences du bloc 1 :

- **Répondre aux incidents et aux demandes** : j'ai répondu à une demande d'évolution formulée par mon responsable, en passant d'un système manuel à un système centralisé.
- **Mettre à disposition un service informatique** : j'ai installé, testé et déployé Xibo, puis formé et accompagné les utilisateurs avec une documentation.
- **Travailler en mode projet** : j'ai découpé le travail en phases — étude, installation, configuration, tests, déploiement, formation — et je les ai menées de manière autonome.
- **Gérer le patrimoine informatique** : j'ai intégré de nouveaux équipements dans le système d'information de l'école — le serveur Docker et les écrans connectés.

---

## RÉALISATION 2 — A2F SUR LE CAS AVEC YUBIKEY (5:30 → 8:30)

### Le besoin

L'ENSP utilise un serveur **CAS Apereo** personnalisé pour l'authentification centralisée de ses services web. Jusqu'à présent, l'accès reposait uniquement sur un couple identifiant/mot de passe. Dans un contexte où les cyberattaques contre les établissements publics se multiplient, mon responsable a souhaité renforcer la sécurité en ajoutant une **authentification à deux facteurs**.

L'objectif : que les utilisateurs, en plus de leur mot de passe, doivent s'identifier avec une **clé physique YubiKey** utilisant le protocole OTP.

### Mon rôle

C'est un projet que je mène. Mon responsable a défini le besoin et validé le choix des YubiKey, mais c'est moi qui réalise l'intégration technique sur le CAS.

### Ma démarche

J'ai commencé par étudier le fonctionnement du CAS Apereo et son système de modules d'authentification. J'ai ensuite étudié le protocole OTP des YubiKey et comment l'intégrer comme second facteur dans le flux d'authentification du CAS.

Pour ne pas impacter l'environnement de production, j'ai mis en place un **site de test** dédié, connecté au CAS, qui me permet de valider le fonctionnement de l'A2F sans risque pour les utilisateurs.

J'en suis actuellement à la **phase de test** : l'authentification par YubiKey fonctionne sur mon environnement de test, mais je n'ai pas encore pu passer en production. C'est un projet en cours.

### Le résultat

> *[Montrer la section parcours ou sécurité du portfolio si pertinent]*

Même si le déploiement en production n'est pas terminé, j'ai déjà des résultats concrets :
- Le module OTP est intégré au CAS de test.
- L'authentification à deux facteurs fonctionne avec les clés YubiKey sur le site de test.
- La prochaine étape sera le déploiement progressif auprès des utilisateurs, en commençant par la DSI.

### Compétences mobilisées

- **Gérer le patrimoine informatique** : j'interviens sur une brique centrale du SI — le serveur d'authentification — pour le faire évoluer.
- **Mettre à disposition un service informatique** : je prépare un service de sécurité renforcée que les utilisateurs utiliseront au quotidien, avec une phase de test rigoureuse avant le déploiement.
- **Travailler en mode projet** : j'ai structuré le travail en phases — étude, intégration, tests, déploiement prévu — et je rends compte de l'avancement à mon responsable.
- **Développer la présence en ligne de l'organisation** : en sécurisant l'authentification des services web de l'école, je contribue à protéger sa présence numérique.

---

## SYNTHÈSE ET BILAN (8:30 → 9:30)

> *[Montrer le portfolio en page d'accueil]*

Pour résumer la couverture des six compétences du bloc 1 :

| Compétence | Xibo | A2F/CAS |
|---|---|---|
| Gérer le patrimoine informatique | ✓ | ✓ |
| Répondre aux incidents et demandes | ✓ | |
| Développer la présence en ligne | | ✓ |
| Travailler en mode projet | ✓ | ✓ |
| Mettre à disposition un service | ✓ | ✓ |
| Organiser son développement professionnel | ✓ | ✓ |

Pour la compétence **"Organiser son développement professionnel"** : sur chacun de ces deux projets, j'ai dû me former en autonomie sur des technologies que je ne connaissais pas — Docker et Xibo d'un côté, le protocole OTP et la configuration avancée de CAS Apereo de l'autre. Je fais de la veille régulière, notamment via GitHub et des documentations officielles, ce qui m'a permis de monter en compétences rapidement.

Ce que ces deux années d'alternance m'ont appris, et qui a changé ma façon de travailler, c'est l'importance de **tester dans un environnement isolé avant de toucher à la production**, et de **documenter et former** pour que le service survive après mon intervention. C'est ce que j'ai fait avec Xibo et c'est ce que je fais avec l'A2F.

Merci, je suis disponible pour vos questions.

---

## AIDE-MÉMOIRE — Questions probables du jury

**Sur Xibo :**
- "Pourquoi Docker et pas une installation classique ?" → Isolation, facilité de sauvegarde et de mise à jour, reproductibilité.
- "Comment vous avez formé les utilisateurs ?" → Doc Word complète + présentation en direct au service comm.
- "Et si le serveur tombe ?" → Les players ont un cache local, ils continuent d'afficher les derniers contenus.
- "3 écrans c'est peu, non ?" → C'est un déploiement progressif, d'autres sont prévus. Le système est dimensionné pour en supporter bien plus.

**Sur l'A2F :**
- "Pourquoi YubiKey et pas une appli TOTP (Google Authenticator) ?" → Clé physique = pas de dépendance au téléphone perso, plus adapté en milieu professionnel, résistant au phishing.
- "C'est pas fini, comment on évalue ?" → La démarche est complète : étude, choix, intégration, tests. Le passage en prod est une question de planning, pas de compétence.
- "Qu'est-ce que CAS Apereo ?" → Serveur d'authentification centralisée open source (Central Authentication Service), utilisé par beaucoup d'universités et établissements publics.
- "Comment vous avez testé ?" → Site dédié connecté au CAS, qui permet de valider l'A2F sans impacter les utilisateurs en production.

**Questions transversales :**
- "Votre veille techno, concrètement ?" → Documentation officielle Xibo/Apereo, GitHub, forums communautaires. J'applique directement ce que j'apprends dans mes projets.
- "Votre plus grosse difficulté ?" → Sur Xibo, la partie réseau (flux entre serveur et players). Sur l'A2F, comprendre le fonctionnement interne du CAS pour y greffer un module.
