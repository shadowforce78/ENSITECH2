using System;

// Données et transformations : aucune de ces méthodes n'affiche quoi que ce soit.
static class Tableau
{
    // 1 + 2. Tableau de dimensions données, rempli de valeurs aléatoires entre min et max (inclus)
    public static int[,] Creer(int lignes, int colonnes, int min = 0, int max = 9)
    {
        Random rnd = new Random();
        int[,] tab = new int[lignes, colonnes];
        for (int i = 0; i < lignes; i++)
            for (int j = 0; j < colonnes; j++)
                tab[i, j] = rnd.Next(min, max + 1);
        return tab;
    }

    // 5. Reste de a / b sans utiliser % (même signe que a, comme l'opérateur)
    public static int Modulo(int a, int b)
    {
        if (b == 0)
            throw new DivideByZeroException("Le diviseur ne peut pas être zéro.");

        int quotient = a / b;
        int reste = a - (quotient * b);
        return reste;
    }

    // 7. 0 devient 1, 1 devient 0
    public static void TransformBinary(int[,] tab)
    {
        for (int i = 0; i < tab.GetLength(0); i++)
            for (int j = 0; j < tab.GetLength(1); j++)
                if (tab[i, j] == 0 || tab[i, j] == 1)
                    tab[i, j] = 1 - tab[i, j];
    }

    // 8. Tri croissant de chaque ligne
    public static void TrierLignes(int[,] tab)
    {
        int[] ligne = new int[tab.GetLength(1)];
        for (int i = 0; i < tab.GetLength(0); i++)
        {
            for (int j = 0; j < ligne.Length; j++) ligne[j] = tab[i, j];
            Array.Sort(ligne);
            for (int j = 0; j < ligne.Length; j++) tab[i, j] = ligne[j];
        }
    }

    // 9. Tri global : on aplatit, on trie, puis on remplit en partant du bas à gauche,
    // vers la droite, puis ligne par ligne vers le haut.
    public static void TrierGlobal(int[,] tab)
    {
        int L = tab.GetLength(0), C = tab.GetLength(1);
        int[] plat = new int[L * C];
        Buffer.BlockCopy(tab, 0, plat, 0, plat.Length * sizeof(int));
        Array.Sort(plat);
        for (int k = 0; k < plat.Length; k++)
            tab[L - 1 - k / C, k % C] = plat[k];
    }

    // 10. Point col = minimum de sa ligne ET maximum de sa colonne
    public static bool EstPointCol(int[,] tab, int i, int j)
    {
        for (int k = 0; k < tab.GetLength(1); k++) if (tab[i, k] < tab[i, j]) return false;
        for (int k = 0; k < tab.GetLength(0); k++) if (tab[k, j] > tab[i, j]) return false;
        return true;
    }

    public static bool ACol(int[,] tab)
    {
        for (int i = 0; i < tab.GetLength(0); i++)
            for (int j = 0; j < tab.GetLength(1); j++)
                if (EstPointCol(tab, i, j)) return true;
        return false;
    }

    // Force un point col en (0,0) : le reste de la ligne 0 est relevé à v, le reste de la colonne 0 abaissé à v
    public static void AjouterPointCol(int[,] tab)
    {
        int v = tab[0, 0];
        for (int k = 1; k < tab.GetLength(1); k++) tab[0, k] = Math.Max(tab[0, k], v);
        for (int k = 1; k < tab.GetLength(0); k++) tab[k, 0] = Math.Min(tab[k, 0], v);
    }
}
