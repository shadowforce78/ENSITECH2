# Chapitre 1 : Les Matrices
## 1) Structure de la matrice  

$M=\begin{pmatrix} 2&3\\4&1\\3&2 \end{pmatrix}$ 

nombre de ligne $n=3$
nombre de colonne $p=2$ 

$m_{1,1}=m_{3,2}=2$
$m_{11}=m_{22} =2$
$m_{3,1}=m_{1,2}=3$ 

### Exo N°1

$B=\begin{pmatrix}3&1&3\\4&2&4\end{pmatrix}$

$a) 3=b_{1,1}=b_{1,3}$
$b) 1=b_{1,2}$
$c)4=b_{2,1}=b_{2,3}$
$d)b_{2,1}+b_{1,3}=7$
$e)b_{2,2}+b^2_{2,3}=4+16=20$ 
$f)b_{1,2}+b_{3,1}=\emptyset$ 

## 2) Type de matrice
### 2.1) Matrice carrée

$$ M_{n} = \begin{pmatrix}
m_{i,j}\\
\end{pmatrix}_{\begin{pmatrix}
1 \lneq i \lneq n \\
1 \lneq j \lneq n 
\end{pmatrix}}$$
Exemple :
$$M_2=\begin{pmatrix}2&7\\8&4 \end{pmatrix}$$
$n=p=2$ 

i = indice de la ligne
j = indice de la colonne

### Exo N°2

Soit la matrice carré génératrice suivante: 
$n=3$
$m_{i,j}=i+(i\times j)+j$

Donner la matrice $M$
$$M=
\begin{pmatrix}
3&5&7\\
5&8&11\\
7&11&15
\end{pmatrix}
$$

### 2.2) Matrice ligne

$$
M=_{1,P}=\begin{pmatrix}m_{i,j}
\end{pmatrix}_{\begin{pmatrix}
i=1 \\
1 \le j \le p
\end{pmatrix}}
$$

Exemple :  $M_{1,2}=\begin{pmatrix}2&7&3\end{pmatrix}$


### 2.3) Matrice colonne

$$M_{n,1}=
\begin{pmatrix}m_{i,j}
\end{pmatrix}_{\begin{pmatrix}
1 \le i \le n  \\
j=1
\end{pmatrix}}$$

Exemple : $M_{3,1}=\begin{pmatrix}2\\4\\7\end{pmatrix}$

### 2.4) Matrice identitée

$I_{2}=\begin{pmatrix}1&0\\0&1\end{pmatrix}$

## 3) Calcul matriciel
### 3.1) Addition / Soustraction

### CONDITION !

$$
A_{n,p}+B_{n\prime, p\prime}=C_{n,p}
$$$\{n=n\prime$
$\{p=p\prime$ 


### 3.2) Produit d'une matrice par un scalaire

Exemple : $$
A=\begin{pmatrix}
3&5\\
4&6\\
-8&2
\end{pmatrix}
$$
$$a = -2$$
$$
a\times A = \begin{pmatrix}
-2\times 3 & -2\times 5 \\
-2 \times 4 & -2\times 2
\end{pmatrix} = \begin{pmatrix} 
-6&-10\\
-8&-12\\
16&-4
\end{pmatrix}
$$

Exo :

$$
A=\begin{pmatrix}
3&4&-6\\
7&-9&8
\end{pmatrix} ; 
B=\begin{pmatrix}
-7&4&-18\\
-9&8&4
\end{pmatrix}
$$
$$3A-2B = 
\begin{pmatrix}
9&12&-18\\
21&-27&24
\end{pmatrix}
+
\begin{pmatrix}
14&-8&36 \\
18&-16&-8
\end{pmatrix}=
\begin{pmatrix}
23&4&18 \\
39&-43&16
\end{pmatrix}$$


Exo :

$$A=
\begin{pmatrix}
-4&9 \\
8&-2 \\
3&4
\end{pmatrix};
B= \begin{pmatrix}
-6&3 \\
-2&4 \\
7&9
\end{pmatrix}; C= \begin{pmatrix}
x&2 \\
4&-5 \\
7&y \\
\end{pmatrix}
$$

Trouver les valeurs de x et y a $10^{-2}$ près tel que :

$$
-2A + 3B - 4C = \begin{pmatrix}
30&\emptyset \\
\emptyset&\emptyset \\
\emptyset&60
\end{pmatrix}
$$
$$
-2A=\begin{pmatrix}
8&-18 \\
-16&4 \\
-6&-8
\end{pmatrix}
$$
$$
3B=\begin{pmatrix}
-18&9 \\
-6&12 \\
21&27
\end{pmatrix}
$$
$$
-4C=\begin{pmatrix}
-4x&-8 \\
-16&20 \\
-28&-4y
\end{pmatrix}
$$

$$
\begin{cases}
8+(-18)+(-4x)=30 \\
-8+27+(-4y)=60
\end{cases}
$$
$$\begin{cases}
-4x=40\\
-4y=41
\end{cases}$$
$$\begin{cases}
x=-10\\
y=-10.25
\end{cases}$$


### 3.3) Multiplication de deux matrices :
<u>CONDITION</u>
$A_{n,p}\times B_{n',p'}=C_{n,p'}$

Exemple : 
$$A=
\begin{pmatrix}
1&2 \\
3&4 \\
5&6
\end{pmatrix} ; B=\begin{pmatrix}
7&8&9 \\
10&11&12
\end{pmatrix}
$$
$$
A_{3,2}\times B_{2,3} = C_{3,3} \implies \begin{pmatrix}
C_{1,1} &C_{1,2}&C_{1,3} \\
C_{2,1}&C_{2,2}&C_{2,3} \\
C_{3,1}&C_{3,2}&C_{3,3}
\end{pmatrix}=\begin{pmatrix}
1\times 7+2\times 10&& 1\times 8 +2\times 11 && 1\times 9 + 2 \times 12 \\
3\times 7 + 4 \times 10 && 3\times 8 + 4\times 11 && 3 \times 9 + 4 \times 12 \\
5 \times 7 + 6 \times 10 && 5 \times 8 + 6 \times 11 && 5 \times 9 + 6 \times 12
\end{pmatrix}
$$

$$
\implies \begin{pmatrix}
27&30&33 \\
61&68&75 \\
95&106&117
\end{pmatrix}
$$
Exemple N°2

$$
A = \begin{pmatrix}
2&0 \\
1&3 \\
4&1
\end{pmatrix};
B = \begin{pmatrix}
3&2&1 \\
0&1&2
\end{pmatrix}
$$

$$
A\times B = \begin{pmatrix}
2\times 3 + 0\times 0 && 2\times 2 + 0\times 1 && 2\times 1 + 0 \times 2 \\
1\times 3 + 3 \times 0 && 1\times 2 + 3 \times 1 && 1 \times 1 + 3 \times 2 \\
4\times 3 + 1 \times 0 && 4\times 2 + 1 \times 1 && 4\times 1 +1\times 2
\end{pmatrix}
$$
$$
= \begin{pmatrix}
6&4&2 \\
3&5&7 \\
12&9&6
\end{pmatrix}
$$
# Chapitre 2 : Congruence
## 1) Introduction

a) $$
17\div5 = 2 (r =3)
$$
b) $$
17=3\times 5 +2
$$

$$
\boxed{17 \equiv 2 [5]}
$$
17 congru à 2 modulo 5


### Exemple 1 : $\text{"Méthode"}$

$273 487 \equiv ? [26]$ 

$\frac{273487}{26}\approx 10518,73$
$273 487 - 26\times 10518 = 19$ 

Sachant que :
$273 487 = 26\times 10518 + 19$
Donc $273487 \equiv 19[26]$ 


### Exemple 2 : $\text{"Méthode"}$ 

$-17 \equiv ? [5]$

$\frac{17}{5}\approx 3,4$  
$17-5\times 3=2$

Sachant que
$17=5\times 3 +2$ 
$17 \equiv 2[5]$
$-17 \equiv -2[5]$ 
$-17\equiv 5-2[5]$
$-17\equiv 3[5]$
Donc $-17\equiv 3 [5]$

### Remarque : Si deux chiffres données ne sont pas multipliable (trop grand)

$2^4 \times 3^3 \equiv ? [5]$ 
1ère etape : 
	$2^4 \equiv ?[5]$
	sachant que $16=5\times 3 +1$
	donc $16 \equiv 1 [5]$

2ème etape :
	$3^3\equiv ? [5]$
	sachant que $27=5\times 5 +2$
	donc $27 \equiv 2[5]$

3ème etape :
	$2^4 \times 3^3 \equiv ? [5]$
	$16\times 27 \equiv 1 \times 2 [5]$
	$16\times 27 \equiv 2[5]$

## Surjection : "Application"

![[Drawing 2026-09-25 09.56.20.excalidraw]]
Chaque image possède au moins 1 antécédent

## Injection : "Application"
![[Drawing 2026-09-25 10.04.49.excalidraw]]
Il existe une ou des images qui n'ont pas d'antécédent

## Bijection "Application"
![[Drawing 2026-09-25 10.08.14.excalidraw]]
Chaque image possède un unique antécédent


## Remarque
### 1) Nombre premiers
	$1;2;3;5;7;11;13;17;19;23$

### 2) Deux nombres premiers entre eux :
	$16= 1\times 2 \times 2 \times 2\times 2$
	$15=1 \times 3 \times 5$
	$PGCD(15;16)=1$


# Chapitre 3: Graphe orienté
## 1) Représentation sagittale

![[Drawing 2026-10-02 08.39.09.excalidraw]]

Remarques :
![[Drawing 2026-10-02 08.46.27.excalidraw]]
Il faut compter toutes les flèches
Sur le graphe précédent il y a 7 flèches

## 2) Propriétées 
1) L'ensemble du Graphe G ou bien définition du Graphe G
	1) G : {(A;A), (A;B), (A;C), (A;D), (B;D), (C;B), (D;C)}
2) Arcs
	1) (B;D), (A;A) => Sont des arcs
3) L'ensemble des sommets
	1) S:{A;B;C;D}
4) Chemin de longueur 3 
	1) chemin de longueur n <=> (n+1) sommets, 
	2) chemin de longueur 3 => 4 sommets
	3) (A;B;D;C)
	4) (A;D;C;B)
	5) (A;A;D;C)
5) Boucle
	1) Une boucle != un circuit
	2) une boucle est un chemin de longueur 1; tel que le sommet de départ = sommet d'arriver (A;A) 
6) Circuit
	1) Un circuit est un chemin de longueur minimum de 2 tel que le sommet de départ est le même sommet d'arrivé
	2) Exemple : 
	3) (C;B;D;C)
	4) (B;D;C;B)
	5) (D;C;B;D) 
7) Chemin Hamiltonien 
	1) un chemin hamiltonien est un chemin passant par tout les sommets une seule fois
	2) (A;B;D;C)
	3) (A;D;C;B)
	4) (A;C;B;D)

## Prédécesseurs / Successeurs


| Sommets | $\gamma +$ | $\gamma -$ |
| ------- | ---------- | ---------- |
| A       | A; B; C; D | A          |
| B       | D          | A; C       |
| C       | B          | A; D       |
| D       | C          | A; B       |
| Total   | 7          | 7          |

## 4 Matrice des adjacente

	$$
	M = \begin{pmatrix}
	1&1&1&1 \\
	0&0&0&1 \\
	0&0&1&0
	\end{pmatrix}
	$$
	Au total le nombre de 1 égale à 7


### Exo

Soit la matrice adjacente M du graphe G

$$
M=\begin{pmatrix}
0&0&0&1&1\\
0&0&1&0&0\\
0&0&0&0&0\\
0&1&0&0&0\\
0&1&1&0&0
\end{pmatrix}
$$

Sommets : 1 = A, 2 = B, 3 = C, 4 = D, 5 = E

**1) Représentation sagittale**

![[Graphe exo sagittal.excalidraw]]

Arcs : A→D, A→E, B→C, D→B, E→B, E→C

**2) Deux chemins de longueur 3**
- (A;D;B;C)
- (A;E;B;C)

**3) Un chemin de longueur 4**

Il n'en existe pas : le graphe n'a aucun circuit ni boucle, et C est un puits (aucun successeur). Les chemins les plus longs sont (A;D;B;C) et (A;E;B;C), de longueur 3.

**4) Tableau $\gamma +$ et $\gamma -$**

| Sommets | $\gamma +$ | $\gamma -$ |
| ------- | ---------- | ---------- |
| A       | D; E       | ∅          |
| B       | C          | D; E       |
| C       | ∅          | B; E       |
| D       | B          | A          |
| E       | B; C       | A          |
| Total   | 6          | 6          |

**5) $M^2$**

Colonnes de $M$ : $C_A=(0,0,0,0,0)$, $C_B=(0,0,0,1,1)$, $C_C=(0,1,0,0,1)$, $C_D=(1,0,0,0,0)$, $C_E=(1,0,0,0,0)$

*Ligne A* $=(0,0,0,1,1)$ :
- $A\to A$ : $0\times0+0\times0+0\times0+1\times0+1\times0 = 0$
- $A\to B$ : $0\times0+0\times0+0\times0+1\times1+1\times1 = 2$
- $A\to C$ : $0\times0+0\times1+0\times0+1\times0+1\times1 = 1$
- $A\to D$ : $0\times1+0\times0+0\times0+1\times0+1\times0 = 0$
- $A\to E$ : $0\times1+0\times0+0\times0+1\times0+1\times0 = 0$

*Ligne B* $=(0,0,1,0,0)$ : elle ne sélectionne que la ligne C de $M$, qui est nulle, donc toute la ligne vaut $(0,0,0,0,0)$.

*Ligne C* $=(0,0,0,0,0)$ : ligne nulle, donc $(0,0,0,0,0)$.

*Ligne D* $=(0,1,0,0,0)$ : elle sélectionne la ligne B de $M$, donc $(0,0,1,0,0)$.

*Ligne E* $=(0,1,1,0,0)$ : ligne B + ligne C de $M$ $=(0,0,1,0,0)+(0,0,0,0,0)=(0,0,1,0,0)$.

Résultat :

$$
M^2=\begin{pmatrix}
0&2&1&0&0\\
0&0&0&0&0\\
0&0&0&0&0\\
0&0&1&0&0\\
0&0&1&0&0
\end{pmatrix}
$$

Nombre total de chemins de longueur 2 : 2 + 1 + 1 + 1 = **5**
(A;D;B), (A;E;B), (A;E;C), (D;B;C), (E;B;C)
