using System;
public class Person
{
    public Person()	// Constructeur par défaut
    {
        name = "";
        Random random = new Random();
        mdp = GeneratePassword(random.Next(10, 30));
    }

    public Person(string leNom) : this()	// Constructeur surchargé (mdp aléatoire)
    {
        name = leNom;
        mdp = GeneratePassword(10);
    }

    public Person(string leNom, String lePass) : this()	// Constructeur surchargé (mdp aléatoire si lePass trop court)
    {
        name = leNom;
        if (lePass.Length >= 10)
            mdp = lePass;
    }

    private string GeneratePassword(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        char[] password = new char[length];
        Random random = new Random();
        for (int i = 0; i < length; i++)
        {
            password[i] = chars[random.Next(chars.Length)];
        }
        return new string(password);
    }

    private string name;
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    private string mdp;
    public string MPD
    {
        get
        {
            return mdp;
        }
        set
        {
            if (value.Length >= 10)
                mdp = value;
        }
    }
}
public class Program
{
    public static void Main()
    {
        Console.WriteLine("---------Construction par défaut--------- ");
        Person UnePersonne = new Person();
        UnePersonne.Name = "John";
        Console.WriteLine("Le nom de la personne est " + UnePersonne.Name);
        Console.WriteLine("Son mot de passe est " + UnePersonne.MPD);
        Console.WriteLine("------Construction Avec accesseur--------- ");
        Person UneAutrePersonne = new Person();
        UneAutrePersonne.Name = "Bill";
        Console.WriteLine("Le nom de la seconde personne est" + UneAutrePersonne.Name);
        Console.WriteLine("Son mot de passe est " + UneAutrePersonne.MPD);
        Console.WriteLine("------Usage de l'accesseur avec une valeur ne respectant pas les restrictions--------- ");
        UneAutrePersonne.MPD = "Bidon";
        Console.WriteLine("Le mot de passe de " + UneAutrePersonne.Name + " est maintenant " + UneAutrePersonne.MPD);
        Console.WriteLine("------Construction Avec construteur surchargé--------- ");
        Person UneDernierePersonne = new Person("Bill", "Bidon");
        Console.WriteLine("Le nom de la dernière personne est " + UneDernierePersonne.Name);
        Console.WriteLine("Son mot de passe est " + UneDernierePersonne.MPD + " (" + UneDernierePersonne.MPD.Length + " caractères)");
        UneDernierePersonne.MPD = "unMotDePasseLong";
        Console.WriteLine("Le mot de passe de " + UneDernierePersonne.Name + " est maintenant " + UneDernierePersonne.MPD);
        Console.WriteLine("------Construction Avec construteur sursurchargé--------- ");
        Person UneDernierePersonne2 = new Person("Bill");
        Console.WriteLine("Le nom de la dernière personne est " + UneDernierePersonne2.Name);
        Console.WriteLine("Son mot de passe est " + UneDernierePersonne2.MPD + " (" + UneDernierePersonne2.MPD.Length + " caractères)");
    }
}
