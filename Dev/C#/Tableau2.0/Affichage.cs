using System;

// Tout ce qui s'affiche (couleurs comprises) vit ici.
static class Affichage
{
    public static void Titre(string t) => Console.WriteLine($"\n===== {t} =====");

    // Largeur de la plus longue valeur, pour aligner les colonnes (les négatifs sont plus larges)
    static int Largeur(int[,] tab)
    {
        int w = 0;
        foreach (int v in tab) w = Math.Max(w, v.ToString().Length);
        return w;
    }

    // 3 + 4 + 7 + 10. Affichage simple ; les cases où `surligne(i, j)` est vrai sont colorées.
    public static void Afficher(int[,] tab, Func<int, int, bool>? surligne = null, ConsoleColor fond = ConsoleColor.Yellow)
    {
        int w = Largeur(tab);
        for (int i = 0; i < tab.GetLength(0); i++)
        {
            for (int j = 0; j < tab.GetLength(1); j++)
            {
                if (surligne != null && surligne(i, j))
                {
                    Console.BackgroundColor = fond;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write(tab[i, j].ToString().PadLeft(w));
                Console.ResetColor();
                Console.Write(' ');
            }
            Console.WriteLine();
        }
    }

    // 6. Pairs en vert, impairs en rouge, zéro sans couleur
    public static void AfficherParite(int[,] tab)
    {
        int w = Largeur(tab);
        for (int i = 0; i < tab.GetLength(0); i++)
        {
            for (int j = 0; j < tab.GetLength(1); j++)
            {
                int v = tab[i, j];
                if (v != 0)
                    Console.ForegroundColor = Tableau.Modulo(v, 2) == 0 ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write(v.ToString().PadLeft(w));
                Console.ResetColor();
                Console.Write(' ');
            }
            Console.WriteLine();
        }
    }

    // 8 + 9. Chaque valeur est colorée sur un dégradé bleu (min) -> rouge (max) (ANSI 24 bits).
    // `min`/`max` fixent l'échelle : avant et après un tri, les couleurs restent comparables.
    public static void AfficherDegrade(int[,] tab, int min, int max)
    {
        int w = Largeur(tab);
        for (int i = 0; i < tab.GetLength(0); i++)
        {
            for (int j = 0; j < tab.GetLength(1); j++)
            {
                int r = max == min ? 0 : (tab[i, j] - min) * 255 / (max - min);
                Console.Write($"\x1b[48;2;{r};0;{255 - r}m\x1b[97m {tab[i, j].ToString().PadLeft(w)} \x1b[0m");
            }
            Console.WriteLine();
        }
    }
}
