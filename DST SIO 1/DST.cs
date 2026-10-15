using System;

class Program
{
    static Random random = new Random();

    // Exercice 2 : procédure équivalente à GeneratePassword
    static void GenererMotDePasse(int longueur, out string motDePasse)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        motDePasse = "";
        for (int i = 0; i < longueur; i++)
        {
            motDePasse += chars[random.Next(chars.Length)];
        }
    }

    // Exercice 3
    static int Numero_Du_Jour(string Lejour)
    {
        switch (Lejour)
        {
            case "Lundi": return 1;
            case "Mardi": return 2;
            case "Mercredi": return 3;
            case "Jeudi": return 4;
            case "Vendredi": return 5;
            case "Samedi": return 6;
            case "Dimanche": return 7;
            default: return -1;
        }
    }

    static void Main()
    {
        // Exercice 1 : boucle corrigée
        string[] jours = new string[] { "Lundi", "Mardi", "Mercredi", "Jeudi", "Vendredi", "Samedi", "Dimanche" };
        int i = 0;
        while (i < jours.Length)
        {
            Console.WriteLine(jours[i]);
            i++;
        }

        // Exercice 2 : test
        string mdp;
        GenererMotDePasse(10, out mdp);
        Console.WriteLine(mdp);

        // Exercice 3 : tests
        Console.WriteLine(Numero_Du_Jour("Lundi"));    // 1
        Console.WriteLine(Numero_Du_Jour("Mardi"));    // 2
        Console.WriteLine(Numero_Du_Jour("Dimanche")); // 7
        Console.WriteLine(Numero_Du_Jour("Truc"));     // -1
    }
}
