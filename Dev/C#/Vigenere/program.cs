using System;

// Program.cs : demande les informations, appelle les méthodes et affiche les résultats.
// Le chiffrement/déchiffrement est dans la classe Vigenere (Vigenere.cs).

Vigenere outil = new Vigenere();

Console.Write("Texte à chiffrer : ");
string texte = Console.ReadLine() ?? "";

Console.Write("Clé : ");
string cle = Console.ReadLine() ?? "";

try
{
    string texteChiffre = outil.Chiffrer(texte, cle);
    Console.WriteLine("Clé superposée : " + outil.SuperposeKey(cle, texte));
    Console.WriteLine("Texte chiffré : " + texteChiffre);

    string texteDechiffre = outil.Dechiffrer(texteChiffre, cle);
    Console.WriteLine("Texte déchiffré : " + texteDechiffre);
}
catch (ArgumentException e)
{
    Console.WriteLine("Erreur : " + e.Message);
}
