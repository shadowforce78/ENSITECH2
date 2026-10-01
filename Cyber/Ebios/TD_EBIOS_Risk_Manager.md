# TD Ebios Risk Manager — Matrice Gravité/Vraisemblance

## Matrice de référence

![[TD Ebios Matrice 10 scenarios.excalidraw]]

- 🟩 Risque Acceptable
- 🟨 Risque Tolérable sous Contrôle
- 🟥 Risque Intolérable

## 1. Placement des scénarios dans la matrice

| #   | Scénario                                                                 | Vraisemblance | Impact | Niveau de risque           |
| --- | ------------------------------------------------------------------------ | :-----------: | :----: | -------------------------- |
| 1   | Intrusion par exploitation d'une vulnérabilité non corrigée              |       4       |   4    | 🟥 Intolérable             |
| 2   | Infection par ransomware via un email malveillant                        |       3       |   4    | 🟥 Intolérable             |
| 3   | Vol de données via un compte utilisateur compromis                       |       3       |   4    | 🟥 Intolérable             |
| 4   | Déni de service distribué (DDoS) sur le site internet public             |       2       |   3    | 🟨 Tolérable sous contrôle |
| 5   | Perte d'un appareil mobile contenant des données sensibles non chiffrées |       3       |   4    | 🟥 Intolérable             |
| 6   | Mauvaise configuration d'un service cloud exposé à Internet              |       3       |   4    | 🟥 Intolérable             |
| 7   | Usurpation d'identité lors d'une connexion à un système critique         |       2       |   4    | 🟨 Tolérable sous contrôle |
| 8   | Destruction de données après sabotage interne                            |       2       |   4    | 🟨 Tolérable sous contrôle |
| 9   | Interception de données via une connexion Wi-Fi non sécurisée            |       3       |   3    | 🟥 Intolérable             |
| 10  | Retard dans le déploiement des correctifs de sécurité critiques          |       4       |   3    | 🟥 Intolérable             |

## 2. Priorités dans la politique de sécurité

**Priorité 1 — Risques intolérables (🟥), à traiter en premier :**
1. Scénario 1 — Intrusion par vulnérabilité non corrigée (V4/I4, le plus critique des deux axes)
2. Scénario 2 — Ransomware via email malveillant
3. Scénario 3 — Vol de données via compte compromis
4. Scénario 5 — Perte d'appareil mobile non chiffré
5. Scénario 6 — Mauvaise configuration cloud
6. Scénario 10 — Retard de déploiement des correctifs
7. Scénario 9 — Interception Wi-Fi non sécurisée

**Priorité 2 — Risques tolérables sous contrôle (🟨), à surveiller et réduire :**
8. Scénario 7 — Usurpation d'identité sur système critique
9. Scénario 8 — Sabotage interne
10. Scénario 4 — DDoS sur le site public

Aucun scénario du TD ne tombe en zone verte : la méthode EBIOS Risk Manager met ici l'accent sur des menaces à fort impact, ce qui est cohérent avec son usage sur les risques stratégiques/critiques d'une organisation.

## 3. Actions proposées par scénario

**1. Intrusion via vulnérabilité non corrigée**
- Processus de patch management avec SLA de correction (ex: 72h pour les failles critiques)
- Scans de vulnérabilités réguliers (Nessus, OpenVAS)
- Segmentation réseau pour limiter la propagation en cas d'exploitation

**2. Ransomware via email malveillant**
- Filtrage anti-phishing avec sandboxing des pièces jointes
- Formation/sensibilisation régulière des utilisateurs
- Sauvegardes hors-ligne régulières et testées (règle 3-2-1)
- EDR sur les postes de travail

**3. Vol de données via compte compromis**
- Authentification multifacteur (MFA) obligatoire
- Politique de mots de passe robuste + gestionnaire de mots de passe
- Supervision des connexions suspectes (SIEM, alertes sur comportements anormaux)

**4. DDoS sur le site public**
- Solution anti-DDoS / CDN (ex: Cloudflare)
- Plan de continuité d'activité pour le service web

**5. Perte d'appareil mobile non chiffré**
- Chiffrement systématique des appareils mobiles (MDM)
- Effacement à distance (remote wipe) en cas de perte/vol
- Politique BYOD stricte avec contrôle d'accès conditionnel

**6. Mauvaise configuration cloud**
- Audit de configuration régulier (CSPM)
- Principe du moindre privilège sur les accès cloud
- Checklist de durcissement avant mise en production

**7. Usurpation d'identité sur système critique**
- MFA renforcée sur les accès aux systèmes critiques
- Journalisation et alerte sur connexions anormales (horaires, géolocalisation)

**8. Sabotage interne**
- Gestion stricte des droits d'accès (moindre privilège, séparation des tâches)
- Sauvegardes régulières avec contrôle d'intégrité
- Traçabilité complète des actions (logs, audit trail)

**9. Interception Wi-Fi non sécurisée**
- VPN obligatoire pour toute connexion depuis un réseau non maîtrisé
- Politique interdisant le traitement de données sensibles sur Wi-Fi public
- Chiffrement systématique des communications (TLS)

**10. Retard de déploiement des correctifs**
- Automatisation du déploiement des correctifs critiques
- SLA de patch formalisé et suivi par le RSSI
- Reporting régulier sur le taux de couverture des correctifs
