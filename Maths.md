# Chapitre 1 : Les MAtrices
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
