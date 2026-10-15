

| Sommets | Prédécesseurs |
| ------- | ------------- |
| A       | ∅             |
| B       | A             |
| C       | A             |
| D       | B             |
| E       | C, D          |
| F       | D, E          |
1) Matrice Adjacente : $$
M=\begin{pmatrix}
0&1&1&0&0&0\\
0&0&0&1&0&0\\
0&0&0&0&1&0\\
0&0&0&0&1&1\\
0&0&0&0&0&1\\
0&0&0&0&0&0
\end{pmatrix}
$$

| Sommets | $\gamma +$ | $\gamma -$ |
| ------- | ---------- | ---------- |
| A       | B/C        | ∅          |
| B       | D          | A          |
| C       | E          | A          |
| D       | E/F        | B          |
| E       | F          | C/D        |
| F       | ∅          | D/E        |
| Total   | 7          | 7          |

2) Niveaux des sommets (niveau = 1 + max des niveaux des prédécesseurs) :

| Sommets | Prédécesseurs | Niveau |
| ------- | ------------- | ------ |
| A       | ∅             | 0      |
| B       | A             | 1      |
| C       | A             | 1      |
| D       | B             | 2      |
| E       | C, D          | 3      |
| F       | D, E          | 4      |

N0 = {A}, N1 = {B, C}, N2 = {D}, N3 = {E}, N4 = {F}

![[Drawing 2026-10-09 10.13.11.excalidraw]]


3) Matrice de la fermeture transitive

$$\hat M = M \lor M^2 \lor M^3 \lor \dots \lor M^k \quad (M^{k+1}=0)$$
$$
\hat M=\begin{pmatrix}
0&1&1&1&1&1\\
0&0&0&1&1&1\\
0&0&0&0&1&1\\
0&0&0&0&1&1\\
0&0&0&0&0&1\\
0&0&0&0&0&0
\end{pmatrix}
$$

4) Le graphe n'a pas de circuit car la diagonale de $\hat M$ ne contient que des 0.
