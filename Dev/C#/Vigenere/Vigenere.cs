using System;

public class Vigenere
{
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
                resultat += (char)('a' + (lettre + decalage) % 26);
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
                int lettre = char.ToLower(texte[i]) - 'a';
                int decalage = char.ToLower(cleRepetee[i]) - 'a';
                resultat += (char)('a' + (lettre - decalage + 26) % 26);
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
