using System;

class Person
{
    private string name = "";
    private string motDePasse = "";

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string MotDePasse
    {
        get { return motDePasse; }
        set
        {
            if (value.Length <= 8)
                throw new ArgumentException("Le mot de passe doit contenir plus de 8 caractères.");
            motDePasse = value;
        }
    }
}

public class Program
{
    public static void Main()
    {
        Person p = new Person();
        p.Name = "John";
        p.MotDePasse = "123456789";
        Console.WriteLine("Le nom de la personne est " + p.Name);
        Console.WriteLine("Son mot de passe est " + p.MotDePasse);

        try { p.MotDePasse = "court"; }
        catch (ArgumentException e) { Console.WriteLine("Refusé : " + e.Message); }
    }
}
