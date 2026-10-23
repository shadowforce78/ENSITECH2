using System;

string[] table =
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

// Étape 1 : écrire la clé "à la queue leu leu" sous le texte (elle n'avance que sur les lettres)
string SuperposeKey(string key, string phrase)
{
    var sb = new System.Text.StringBuilder();
    int k = 0;
    foreach (char c in phrase)
        sb.Append(char.IsAsciiLetter(c) ? key[k++ % key.Length] : c);
    return sb.ToString();
}

// Étapes 2-3 : lettre du texte + lettre de la clé = coordonnées, on lit la case de la grille
string Chiffrer(string key, string phrase)
{
    string superposed = SuperposeKey(key, phrase);
    var sb = new System.Text.StringBuilder();
    for (int i = 0; i < phrase.Length; i++)
        sb.Append(char.IsAsciiLetter(phrase[i])
            ? table[char.ToLower(superposed[i]) - 'a'][char.ToLower(phrase[i]) - 'a']
            : phrase[i]);
    return sb.ToString();
}

// Étape 4 : sens inverse, on prend la ligne de la clé et on cherche la lettre chiffrée
string Dechiffrer(string key, string chiffre)
{
    string superposed = SuperposeKey(key, chiffre);
    var sb = new System.Text.StringBuilder();
    for (int i = 0; i < chiffre.Length; i++)
        sb.Append(char.IsAsciiLetter(chiffre[i])
            ? (char)('a' + table[char.ToLower(superposed[i]) - 'a'].IndexOf(char.ToLower(chiffre[i])))
            : chiffre[i]);
    return sb.ToString();
}

string message = "le message est la";
string cle = "lacleestla";

Console.WriteLine(message);
Console.WriteLine(SuperposeKey(cle, message));
string chiffre = Chiffrer(cle, message);
Console.WriteLine(chiffre);
Console.WriteLine(Dechiffrer(cle, chiffre));
