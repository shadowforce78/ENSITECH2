using System;

class Person
{
    private static readonly Random random = new Random();
    private string name = "";
    private string mdp = "";

    public Person()                                  // Constructeur par défaut
    {
        name = "";
        MPD = GeneratePassword(random.Next(10, 30));
    }

    public Person(string leNom, string lePass)       // Constructeur surchargé
    {
        Name = leNom;
        MPD = lePass;
    }

    private static string GeneratePassword(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        char[] password = new char[length];
        for (int i = 0; i < length; i++)
            password[i] = chars[random.Next(chars.Length)];
        return new string(password);
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string MPD
    {
        get { return mdp; }
        set
        {
            mdp = value.Length >= 10 ? value : GeneratePassword(10);
        }
    }
}

public class Program
{
    public static void Main()
    {
        Person p1 = new Person();
        p1.Name = "John";
        Console.WriteLine("Le nom de la personne est " + p1.Name);
        Console.WriteLine("Son mot de passe est " + p1.MPD);
        Console.WriteLine("-------------------------");

        Person p2 = new Person("Bill", "Bidon");
        Console.WriteLine("Le nom de la seconde personne est " + p2.Name);
        Console.WriteLine("Son mot de passe est " + p2.MPD + " (" + p2.MPD.Length + " caractères)");

        p2.MPD = "unMotDePasseLong";
        Console.WriteLine("Le mot de passe de " + p2.Name + " est maintenant " + p2.MPD);
    }
}
