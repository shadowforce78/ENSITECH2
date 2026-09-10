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