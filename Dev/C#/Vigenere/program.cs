using System;

Vigenere outil = new Vigenere();

Console.Write("Texte : ");
string texte = Console.ReadLine() ?? "";

Console.Write("Clé : ");
string cle = Console.ReadLine() ?? "";

Console.Write("1) Chiffrer  2) Déchiffrer : ");
bool dechiffrer = Console.ReadLine()?.Trim() == "2";

Console.Write("Version d'affichage (1 table, 2 trois lignes, 3 les deux) : ");
string version = Console.ReadLine()?.Trim() ?? "";

Console.Write("Délai en ms entre les lettres (0 = une touche par lettre) [400] : ");
Affichage.Delai = int.TryParse(Console.ReadLine(), out int delai) ? delai : 400;

try
{
    string sortie = dechiffrer ? outil.Dechiffrer(texte, cle) : outil.Chiffrer(texte, cle);
    Affichage.Animer(outil, texte, outil.SuperposeKey(cle, texte), sortie, dechiffrer, version);
}
catch (ArgumentException e)
{
    Console.WriteLine("Erreur : " + e.Message);
}
