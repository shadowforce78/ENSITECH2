using System;
using System.Text;

/// <summary>
/// Chiffrement / déchiffrement de Vigenère.
/// Cette classe ne fait aucune saisie ni aucun affichage : elle reçoit des données et retourne un résultat.
/// </summary>
public class Vigenere
{
    private readonly string[] table =
    {
        "abcdefghijklmnopqrstuvwxyz",
        "bcdefghijklmnopqrstuvwxyza",
        "cdefghijklmnopqrstuvwxyzab",
        "defghijklmnopqrstuvwxyzabc",
        "efghijklmnopqrstuvwxyzabcd",
        "fghijklmnopqrstuvwxyzabcde",
        "ghijklmnopqrstuvwxyzabcdef",
        "hijklmnopqrstuvwxyzabcdefg",
        "ijklmnopqrstuvwxyzabcdefgh",
        "jklmnopqrstuvwxyzabcdefghi",
        "klmnopqrstuvwxyzabcdefghij",
        "lmnopqrstuvwxyzabcdefghijk",
        "mnopqrstuvwxyzabcdefghijkl",
        "nopqrstuvwxyzabcdefghijklm",
        "opqrstuvwxyzabcdefghijklmn",
        "pqrstuvwxyzabcdefghijklmno",
        "qrstuvwxyzabcdefghijklmnop",
        "rstuvwxyzabcdefghijklmnopq",
        "stuvwxyzabcdefghijklmnopqr",
        "tuvwxyzabcdefghijklmnopqrs",
        "uvwxyzabcdefghijklmnopqrst",
        "vwxyzabcdefghijklmnopqrstu",
        "wxyzabcdefghijklmnopqrstuv",
        "xyzabcdefghijklmnopqrstuvw",
        "yzabcdefghijklmnopqrstuvwx",
        "zabcdefghijklmnopqrstuvwxy",
    };

    private string cle = "";

    /// <summary>Crée un outil sans clé par défaut (la clé est passée à chaque appel).</summary>
    public Vigenere()
    {
    }

    /// <summary>Crée un outil avec une clé par défaut.</summary>
    public Vigenere(string cle)
    {
        Cle = cle;
    }

    /// <summary>Clé par défaut, utilisée par les surcharges sans paramètre de clé.</summary>
    public string Cle
    {
        get => cle;
        set
        {
            VerifierCle(value);
            cle = value;
        }
    }

    // Étape 1 : écrire la clé "à la queue leu leu" sous le texte (elle n'avance que sur les lettres)
    public string SuperposeKey(string key, string phrase)
    {
        VerifierCle(key);
        var sb = new StringBuilder();
        int k = 0;
        foreach (char c in phrase)
            sb.Append(char.IsAsciiLetter(c) ? key[k++ % key.Length] : c);
        return sb.ToString();
    }

    // Étapes 2-3 : lettre du texte + lettre de la clé = coordonnées, on lit la case de la grille
    public string Chiffrer(string phrase, string key)
    {
        string superposed = SuperposeKey(key, phrase);
        var sb = new StringBuilder();
        for (int i = 0; i < phrase.Length; i++)
            sb.Append(char.IsAsciiLetter(phrase[i])
                ? table[char.ToLower(superposed[i]) - 'a'][char.ToLower(phrase[i]) - 'a']
                : phrase[i]);
        return sb.ToString();
    }

    // Étape 4 : sens inverse, on prend la ligne de la clé et on cherche la lettre chiffrée
    public string Dechiffrer(string chiffre, string key)
    {
        string superposed = SuperposeKey(key, chiffre);
        var sb = new StringBuilder();
        for (int i = 0; i < chiffre.Length; i++)
            sb.Append(char.IsAsciiLetter(chiffre[i])
                ? (char)('a' + table[char.ToLower(superposed[i]) - 'a'].IndexOf(char.ToLower(chiffre[i])))
                : chiffre[i]);
        return sb.ToString();
    }

    // Surcharges utilisant la clé stockée dans l'objet
    public string Chiffrer(string phrase) => Chiffrer(phrase, cle);

    public string Dechiffrer(string chiffre) => Dechiffrer(chiffre, cle);

    private static void VerifierCle(string key)
    {
        if (string.IsNullOrEmpty(key))
            throw new ArgumentException("La clé ne doit pas être vide.");
        foreach (char c in key)
            if (!char.IsAsciiLetter(c))
                throw new ArgumentException("La clé ne doit contenir que des lettres (a-z).");
    }
}
