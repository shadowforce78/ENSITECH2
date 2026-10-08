using System;

// Menu principal : saisie du tableau, puis sous-menu des transformations.
// Pour ajouter une action : écrire sa méthode ici et ajouter UNE ligne dans `actions`.
static class Menu
{
    static int[,] tab = new int[0, 0];

    static readonly (string Nom, Action Executer)[] actions =
    {
        ("Rechercher une valeur (Q4)",              Rechercher),
        ("Pairs / impairs en couleur (Q6)",         Parite),
        ("TransformBinary : 0 <-> 1 (Q7)",          Binaire),
        ("Tri ligne par ligne (Q8)",                TriLignes),
        ("Tri global (Q9)",                         TriGlobal),
        ("Points cols (Q10)",                       PointsCols),
        ("Ajouter un point col s'il n'y en a pas",  AjouterPointCol),
        ("Réafficher le tableau",                   () => Affichage.Afficher(tab)),
        ("Nouveau tableau",                         Creer),
    };

    public static void Main()
    {
        Creer();
        while (true)
        {
            Affichage.Titre("Transformations");
            for (int i = 0; i < actions.Length; i++)
                Console.WriteLine($"{i + 1}. {actions[i].Nom}");
            Console.WriteLine("0. Quitter");

            int choix = Lire("Votre choix : ", 0);
            if (choix == 0) return;
            if (choix <= actions.Length) actions[choix - 1].Executer();
        }
    }

    // Saisie d'un entier >= min (quitte proprement si l'entrée est fermée)
    static int Lire(string message, int min = 1)
    {
        int n;
        Console.Write(message);
        string? ligne;
        while ((ligne = Console.ReadLine()) != null && (!int.TryParse(ligne, out n) || n < min))
            Console.Write("Entier valide attendu : ");
        if (ligne == null) Environment.Exit(0);
        return int.Parse(ligne!);
    }

    // Copie du tableau avant une transformation, pour afficher l'état « avant ».
    static int[,] Copie() => (int[,])tab.Clone();

    static void Creer()
    {
        int lignes = Lire("Nombre de lignes : "), colonnes = Lire("Nombre de colonnes : ");
        int min = Lire("Valeur minimale (ex: -9) : ", int.MinValue);
        int max = Lire("Valeur maximale (ex: 9) : ", min);
        tab = Tableau.Creer(lignes, colonnes, min, max);
        Affichage.Titre("Tableau");
        Affichage.Afficher(tab);
    }

    static void Rechercher()
    {
        int val = Lire("Valeur à rechercher : ", int.MinValue);
        Affichage.Titre($"Recherche de {val}");
        Affichage.Afficher(tab, (i, j) => tab[i, j] == val);
    }

    static void Parite()
    {
        Affichage.Titre("Pairs (vert) / impairs (rouge)");
        Affichage.AfficherParite(tab);
    }

    static void Binaire()
    {
        int[,] avant = Copie();
        Tableau.TransformBinary(tab);
        Affichage.Titre("Avant TransformBinary");
        Affichage.Afficher(avant);
        Affichage.Titre("Après TransformBinary (cyan = valeur inversée)");
        Affichage.Afficher(tab, (i, j) => tab[i, j] != avant[i, j], ConsoleColor.Cyan);
    }

    static void TriLignes() => Trier("ligne par ligne", Tableau.TrierLignes);
    static void TriGlobal() => Trier("global (petit en bas à gauche, grand en haut à droite)", Tableau.TrierGlobal);

    static void Trier(string nom, Action<int[,]> tri)
    {
        int[,] avant = Copie();
        tri(tab);
        int min = int.MaxValue, max = int.MinValue;
        foreach (int v in tab) { min = Math.Min(min, v); max = Math.Max(max, v); }
        Affichage.Titre("Avant tri (dégradé bleu -> rouge)");
        Affichage.AfficherDegrade(avant, min, max);
        Affichage.Titre($"Après tri {nom}");
        Affichage.AfficherDegrade(tab, min, max);
    }

    static void PointsCols()
    {
        Affichage.Titre("Points cols (magenta)");
        Affichage.Afficher(tab, (i, j) => Tableau.EstPointCol(tab, i, j), ConsoleColor.Magenta);
    }

    static void AjouterPointCol()
    {
        if (Tableau.ACol(tab)) { Console.WriteLine("Le tableau a déjà un point col."); return; }
        Tableau.AjouterPointCol(tab);
        Affichage.Titre("Point col ajouté en (0,0)");
        PointsCols();
    }
}
