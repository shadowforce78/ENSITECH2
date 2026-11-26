using System;
using System.Threading;

// Trois versions de l'animation pas à pas (1 = table, 2 = trois lignes, 3 = les deux).
// On en gardera une seule : supprimer les autres méthodes V*.
public static class Affichage
{
    public static int Delai = 400; // ms entre deux lettres, 0 = attendre une touche

    const int TX = 4, TY = 1; // coin haut-gauche des cases de la table
    static char[,] T = null!;

    public static void Animer(Vigenere v, string entree, string cleRep, string sortie, bool dech, string version)
    {
        T = v.Table;
        entree = entree.ToLower(); cleRep = cleRep.ToLower(); sortie = sortie.ToLower();

        int besoin = version == "1" ? 30 : version == "3" ? 33 : 0;
        if (Console.WindowHeight < besoin)
        {
            Console.WriteLine($"Terminal trop petit ({besoin} lignes requises) -> version 2.");
            Thread.Sleep(1500);
            version = "2";
        }

        string[] noms = dech ? new[] { "Chiffré", "Clé", "Déchiffré" } : new[] { "Texte", "Clé", "Chiffré" };
        Console.Clear();
        Console.CursorVisible = false;
        int bas;
        try
        {
            bas = version == "1" ? V1(entree, cleRep, sortie, dech)
                : version == "3" ? V3(noms, entree, cleRep, sortie, dech)
                : V2(noms, entree, cleRep, sortie, dech);
        }
        finally { Console.ResetColor(); Console.CursorVisible = true; }

        Console.SetCursorPosition(0, bas);
        Console.WriteLine("Résultat : " + sortie);
    }

    // ---- Version 1 : la table seule, ligne jaune / colonne cyan / case verte ----
    static int V1(string e, string k, string s, bool dech)
    {
        DessineTable();
        int r0 = -1, c0 = -1;
        for (int i = 0; i < e.Length; i++)
        {
            if (!char.IsAsciiLetter(e[i])) continue;
            int r = k[i] - 'a', c = (dech ? s[i] : e[i]) - 'a';
            Efface(r0, c0);
            Surligne(r, c, 3);
            Info(TY + 27, Explication(e, k, s, dech, i));
            Pause();
            r0 = r; c0 = c;
        }
        return TY + 29;
    }

    // ---- Version 2 : texte / clé / résultat alignés, révélés lettre par lettre ----
    static int V2(string[] noms, string e, string k, string s, bool dech)
    {
        for (int i = 0; i < e.Length; i++)
        {
            if (!char.IsAsciiLetter(e[i])) continue;
            Lignes(0, noms, e, k, s, i, i, Explication(e, k, s, dech, i));
            Pause();
        }
        Lignes(0, noms, e, k, s, e.Length, e.Length, "");
        return 5;
    }

    // ---- Version 3 : table + trois lignes, en 3 temps (ligne clé, colonne clair, résultat) ----
    static int V3(string[] noms, string e, string k, string s, bool dech)
    {
        const int Y = TY + 27; // les trois lignes sous la table
        DessineTable();
        int r0 = -1, c0 = -1;
        for (int i = 0; i < e.Length; i++)
        {
            if (!char.IsAsciiLetter(e[i])) continue;
            int r = k[i] - 'a', c = (dech ? s[i] : e[i]) - 'a';
            Efface(r0, c0);
            Surligne(r, c, 1); Lignes(Y, noms, e, k, s, i, i - 1, "1) ligne de la clé '" + k[i] + "'"); Thread.Sleep(Delai / 3);
            Surligne(r, c, 2); Lignes(Y, noms, e, k, s, i, i - 1, "2) colonne de la lettre '" + (dech ? s[i] : e[i]) + "'"); Thread.Sleep(Delai / 3);
            Surligne(r, c, 3); Lignes(Y, noms, e, k, s, i, i, Explication(e, k, s, dech, i));
            Pause();
            r0 = r; c0 = c;
        }
        Lignes(Y, noms, e, k, s, e.Length, e.Length, "");
        return Y + 4;
    }

    // ---- Briques communes ----

    static string Explication(string e, string k, string s, bool dech, int i)
    {
        char clair = dech ? s[i] : e[i], chiffre = dech ? e[i] : s[i];
        return $"ligne '{k[i]}' (clé)  x  colonne '{clair}' (clair)  ->  '{chiffre}' (chiffré)";
    }

    static void Pause()
    {
        if (Delai == 0) Console.ReadKey(true); else Thread.Sleep(Delai);
    }

    static void Couleur(ConsoleColor fg, ConsoleColor? bg = null)
    {
        Console.ResetColor();
        Console.ForegroundColor = fg;
        if (bg != null) Console.BackgroundColor = bg.Value;
    }

    static void Info(int y, string texte)
    {
        Console.SetCursorPosition(0, y);
        Couleur(ConsoleColor.White);
        Console.Write(texte.PadRight(Console.WindowWidth - 1));
    }

    static void Case(int r, int c, ConsoleColor fg, ConsoleColor? bg = null)
    {
        Console.SetCursorPosition(TX + c * 2, TY + r);
        Couleur(fg, bg);
        Console.Write(T[r, c]);
    }

    static void TeteCol(int c, ConsoleColor f)
    {
        Console.SetCursorPosition(TX + c * 2, 0);
        Couleur(f);
        Console.Write((char)('a' + c));
    }

    static void TeteLig(int r, ConsoleColor f)
    {
        Console.SetCursorPosition(1, TY + r);
        Couleur(f);
        Console.Write((char)('a' + r));
    }

    static void DessineTable()
    {
        for (int i = 0; i < 26; i++) { TeteCol(i, ConsoleColor.DarkGray); TeteLig(i, ConsoleColor.DarkGray); }
        for (int r = 0; r < 26; r++)
            for (int c = 0; c < 26; c++)
                Case(r, c, ConsoleColor.Gray);
    }

    // niveau 1 = ligne, 2 = + colonne, 3 = + case d'intersection
    static void Surligne(int r, int c, int niveau)
    {
        for (int x = 0; x < 26; x++) Case(r, x, ConsoleColor.Yellow);
        TeteLig(r, ConsoleColor.Yellow);
        if (niveau >= 2)
        {
            for (int y = 0; y < 26; y++) Case(y, c, ConsoleColor.Cyan);
            TeteCol(c, ConsoleColor.Cyan);
        }
        if (niveau >= 3) Case(r, c, ConsoleColor.Black, ConsoleColor.Green);
    }

    static void Efface(int r, int c)
    {
        if (r < 0) return;
        for (int x = 0; x < 26; x++) Case(r, x, ConsoleColor.Gray);
        for (int y = 0; y < 26; y++) Case(y, c, ConsoleColor.Gray);
        TeteLig(r, ConsoleColor.DarkGray);
        TeteCol(c, ConsoleColor.DarkGray);
    }

    // Trois lignes sur y..y+2, info sur y+3. cur = lettre courante, rev = dernier indice du résultat déjà montré.
    // Si le texte dépasse la largeur du terminal, on affiche une fenêtre glissante autour de cur.
    static void Lignes(int y, string[] noms, string e, string k, string s, int cur, int rev, string info)
    {
        int n = Math.Max(4, (Console.WindowWidth - 14) / 2);
        int debut = Math.Clamp(cur - n / 2, 0, Math.Max(0, e.Length - n));
        int fin = Math.Min(e.Length, debut + n);
        string[] src = { e, k, s };
        ConsoleColor[] coul = { ConsoleColor.Cyan, ConsoleColor.Yellow, ConsoleColor.Green };

        for (int l = 0; l < 3; l++)
        {
            Console.SetCursorPosition(0, y + l);
            Couleur(ConsoleColor.Gray);
            Console.Write(noms[l].PadRight(10) + ": ");
            for (int i = debut; i < fin; i++)
            {
                bool lettre = char.IsAsciiLetter(e[i]);
                bool cache = l == 2 && lettre && i > rev;
                if (!lettre || cache) Couleur(ConsoleColor.DarkGray);
                else if (i == cur) Couleur(ConsoleColor.Black, coul[l]);
                else Couleur(coul[l]);
                Console.Write(cache ? '·' : src[l][i]);
                Couleur(ConsoleColor.Gray);
                Console.Write(' ');
            }
            Console.Write(new string(' ', Math.Max(0, Console.WindowWidth - 1 - 12 - (fin - debut) * 2)));
        }
        Info(y + 3, info);
    }
}
