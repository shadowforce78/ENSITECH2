using System;

public class Vigenere
{
    // Tableau de Vigenère 26x26 : Table[ligne = lettre clé, colonne = lettre texte]
    public char[,] Table { get; } = new char[26, 26];

    public Vigenere()
    {
        for (int l = 0; l < 26; l++)
            for (int c = 0; c < 26; c++)
                Table[l, c] = (char)('a' + (l + c) % 26);
    }

    // Répète la clé sous le texte (les non-lettres sont gardées telles quelles)
    public string SuperposeKey(string cle, string texte)
    {
        VerifierCle(cle);
        string resultat = "";
        int position = 0; // position dans la clé

        for (int i = 0; i < texte.Length; i++)
        {
            if (char.IsAsciiLetter(texte[i]))
            {
                resultat += cle[position % cle.Length];
                position++;
            }
            else
            {
                resultat += texte[i];
            }
        }
        return resultat;
    }

    public string Chiffrer(string texte, string cle)
    {
        string cleRepetee = SuperposeKey(cle, texte);
        string resultat = "";

        for (int i = 0; i < texte.Length; i++)
        {
            if (char.IsAsciiLetter(texte[i]))
            {
                int lettre = char.ToLower(texte[i]) - 'a';       // 0 à 25
                int decalage = char.ToLower(cleRepetee[i]) - 'a'; // 0 à 25
                resultat += Table[decalage, lettre];
            }
            else
            {
                resultat += texte[i];
            }
        }
        return resultat;
    }

    public string Dechiffrer(string texte, string cle)
    {
        string cleRepetee = SuperposeKey(cle, texte);
        string resultat = "";

        for (int i = 0; i < texte.Length; i++)
        {
            if (char.IsAsciiLetter(texte[i]))
            {
                int decalage = char.ToLower(cleRepetee[i]) - 'a';
                // on cherche la lettre chiffrée dans la ligne de la clé : sa colonne = lettre claire
                int col = 0;
                while (Table[decalage, col] != char.ToLower(texte[i])) col++;
                resultat += (char)('a' + col);
            }
            else
            {
                resultat += texte[i];
            }
        }
        return resultat;
    }

    // La clé doit être non vide et contenir seulement des lettres
    private void VerifierCle(string cle)
    {
        if (string.IsNullOrEmpty(cle))
            throw new ArgumentException("La clé ne doit pas être vide.");

        for (int i = 0; i < cle.Length; i++)
        {
            if (!char.IsAsciiLetter(cle[i]))
                throw new ArgumentException("La clé ne doit contenir que des lettres (a-z).");
        }
    }
}
