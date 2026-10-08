using System;

class Program
{
    static void Main()
    {
        // 1. Dimensions choisies
        int[,] tab = new int[Lire("Nombre de lignes : "), Lire("Nombre de colonnes : ")];

        Remplir(tab);
        Afficher(tab);

        int val = Lire("Valeur à rechercher : ");
        Afficher(tab, val);

        Console.WriteLine(Modulo(5, 3)); // 2
    }

    static int Lire(string message)
    {
        int n;
        Console.Write(message);
        while (!int.TryParse(Console.ReadLine(), out n) || n < 0)
            Console.Write("Entier positif attendu : ");
        return n;
    }



    // 2. Valeurs aléatoires entre 0 et 9
    static void Remplir(int[,] tab)
    {
        Random rnd = new Random();
        for (int i = 0; i < tab.GetLength(0); i++)
            for (int j = 0; j < tab.GetLength(1); j++)
                tab[i, j] = rnd.Next(0, 10);
    }

    // 3 + 4. Affichage ; les cases égales à "cherche" sont colorées en jaune
    static void Afficher(int[,] tab, int? cherche = null)
    {
        for (int i = 0; i < tab.GetLength(0); i++)
        {
            for (int j = 0; j < tab.GetLength(1); j++)
            {
                if (tab[i, j] == cherche)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write(tab[i, j]);
                Console.ResetColor();
                Console.Write(' ');
            }
            Console.WriteLine();
        }
    }

    // 5. Reste de a / b sans utiliser % (même signe que a, comme l'opérateur)
    static int Modulo(int a, int b)
    {
        if (b == 0)
            throw new DivideByZeroException("Le diviseur ne peut pas être zéro.");

        int quotient = a / b;
        int reste = a - (quotient * b);
        return reste;
    }
}