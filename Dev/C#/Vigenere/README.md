# Vigenère — affichage graphique en terminal

## 1. Le principe du chiffrement (à savoir par cœur)

- On répète la **clé** sous le texte, lettre par lettre (les non-lettres sont ignorées et ne consomment pas la clé).
- On utilise le **tableau de Vigenère** 26×26 : `Table[ligne, colonne]`.
  - **ligne** = lettre de la clé
  - **colonne** = lettre claire
  - **case** = lettre chiffrée → `Table[l, c] = (char)('a' + (l + c) % 26)`
- **Déchiffrer** : on prend la ligne de la clé, on cherche la lettre chiffrée dedans, sa **colonne** donne la lettre claire.

Exemple, clé `limon`, texte `attaque` :

| texte | a | t | t | a | q | u | e |
|---|---|---|---|---|---|---|---|
| clé   | l | i | m | o | n | l | i |
| chiffré | l | b | f | o | d | f | m |

## 2. Organisation du code

| Fichier | Rôle |
|---|---|
| `Vigenere.cs` | La logique : `Table`, `SuperposeKey`, `Chiffrer`, `Dechiffrer`. Aucun affichage. |
| `Affichage.cs` | Le dessin en terminal (couleurs + animation). |
| `program.cs` | Pose les questions, appelle `Chiffrer`/`Dechiffrer`, puis `Affichage.Animer`. |

Idée à retenir : **la logique et l'affichage sont séparés**. `Affichage` reçoit trois chaînes
de même longueur (texte, clé répétée, résultat) et les dessine, sans rien calculer.

## 3. Ce qu'on surligne pour la lettre `i`

| Élément | Couleur | Chiffrement | Déchiffrement |
|---|---|---|---|
| Ligne | jaune | lettre de la clé | lettre de la clé |
| Colonne | cyan | lettre claire (`texte[i]`) | lettre claire (`sortie[i]`) |
| Case | vert | lettre chiffrée (`sortie[i]`) | lettre chiffrée (`texte[i]`) |

En code :

```csharp
int r = k[i] - 'a', c = (dech ? s[i] : e[i]) - 'a';   // indices dans Table[r, c]
```

`'x' - 'a'` transforme une lettre en nombre de 0 à 25.

## 4. Techniques de console utilisées

- `Console.SetCursorPosition(x, y)` : place le curseur. La case `(r, c)` de la table est à
  `x = 4 + c*2` et `y = 1 + r`.
- `Console.ForegroundColor` / `BackgroundColor` : couleur du texte / du fond.
- `Console.ResetColor()` : revient aux couleurs par défaut (évite qu'un fond reste collé).
- `Console.CursorVisible = false` : cache le curseur pendant l'animation (remis dans un `finally`).
- `Thread.Sleep(ms)` : pause entre deux étapes. `Console.ReadKey(true)` : attend une touche
  sans l'afficher (mode manuel, délai = 0).
- `Console.WindowHeight` / `WindowWidth` : taille du terminal, pour savoir si l'affichage tient.

### Pourquoi ça ne clignote pas

On dessine la table **une seule fois**. À chaque lettre on ne repeint que ce qui change :

1. `Efface` : remet en gris la ligne et la colonne de la lettre précédente ;
2. `Surligne` : colore la nouvelle ligne et la nouvelle colonne.

## 5. Les trois versions de l'animation

| Version | Contenu | Terminal minimum |
|---|---|---|
| 1 | Table seule : ligne jaune, colonne cyan, case verte + phrase d'explication | 30 lignes |
| 2 | Trois lignes alignées (texte / clé / résultat), lettre courante surlignée, résultat révélé peu à peu (`·` = pas encore calculé) | — |
| 3 | Table + trois lignes, en 3 temps par lettre : ligne → colonne → case | 33 lignes |

La version 3 utilise le paramètre `niveau` de `Surligne` (1 = ligne, 2 = + colonne,
3 = + case d'intersection). Si le terminal est trop petit, on retombe sur la version 2.

## 6. Points d'attention (questions possibles à l'oral)

- **Clé invalide** (vide ou avec autre chose que des lettres) → `ArgumentException` levée par
  `VerifierCle`, attrapée dans `program.cs`.
- **Non-lettres** (espaces, ponctuation) : gardées telles quelles, en gris, sans pause, et la clé
  n'avance pas.
- **Minuscules** : tout est passé en minuscules car `Table` contient des minuscules.
- **Texte plus large que le terminal** : fenêtre glissante autour de la lettre courante, sinon la
  ligne passerait à la suivante et les positions du curseur seraient fausses.
- **Pas de bibliothèque externe** : tout vient de `System.Console`, donc rien à installer.

## 7. Lancer

```bash
dotnet run
```

Questions posées : texte, clé, chiffrer/déchiffrer, version (1/2/3), délai en ms
(`0` = une touche par lettre).
