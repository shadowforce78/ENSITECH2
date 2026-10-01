using System; 
namespace RemplirTab1D 
{ 
    class MainClass 
    { 
        static void RemlirTab(ref int[] Tablo, int taille, int min, int max) 
        { 
            Random Aleatoire = new Random(); 
            for (int i = 0; i < taille; i++) 
            { 
                Tablo[i] = Aleatoire.Next(min, max);  
            } 
        } 
        static void AfficheTab(int[] Tablo, int taille) 
        { 
            for (int i = 0; i < taille; i++) 
            { 
                Console.Write(Tablo[i]+" "); 
            } 
            Console.WriteLine(""); 
        } 
 
        static int FindMin(int[] Tablo, int taille) 
        { 
            int TheMin = Tablo[0]; 
            for (int i = 1; i < taille; i++) 
            { 
                if (Tablo[i] <TheMin) 
                    TheMin=Tablo[i]; 
            } 
            return TheMin; 
        } 
 
 
        static int FindMax(int[] Tablo, int taille) 
        { 
            int TheMax = Tablo[0]; 
            for (int i = 1; i < taille; i++) 
            { 
                if (Tablo[i] > TheMax) 
                    TheMax = Tablo[i]; 
            } 
            return TheMax; 
        } 

        static int FindMoyenne(int[] Tablo, int taille) 
        { 
            int Somme = 0; 
            for (int i = 0; i < taille; i++) 
            { 
                Somme += Tablo[i]; 
            } 
            return Somme / taille; 
        }

        static void Sortab(ref int[] Tablo, int taille) 
        { 
            for (int i = 0; i < taille; i++) 
            { 
                for (int j = i + 1; j < taille; j++) 
                    if (Tablo[j] < Tablo[i]) 
                    { 
                        int temp = Tablo[i]; 
                        Tablo[i] = Tablo[j];  
                        Tablo[j] = temp;  
                    } 
            } 
        } 

        static void SupprimerValeur(ref int[] Tablo, int valeurASupprimer) 
        { 
            int taille = Tablo.Length; 
            int[] nouveauTablo = new int[taille]; 
            int index = 0; 
            for (int i = 0; i < taille; i++) 
            { 
                try
                { 
                    if (Tablo[i] != valeurASupprimer) 
                    { 
                        nouveauTablo[index] = Tablo[i]; 
                        index++; 
                    } 
                } 
                catch (IndexOutOfRangeException e) 
                { 
                    Console.WriteLine("Erreur : " + e.Message); 
                }
            } 
            Array.Resize(ref nouveauTablo, index); 
            Tablo = nouveauTablo;
        }

        static void DeplacerPivot(ref int[] Tablo, int pivot) 
        { 
            int taille = Tablo.Length;
            int[] nouveauTablo = new int[taille];
            int index = 0; // prochaine case libre dans le nouveau tableau

            // 1er passage : on recopie d'abord toutes les valeurs < pivot (en tête)
            for (int i = 0; i < taille; i++)
            {
                if (Tablo[i] < pivot)
                {
                    nouveauTablo[index] = Tablo[i];
                    index++;
                }
            }
            // 2e passage : puis toutes les valeurs >= pivot (en queue)
            for (int i = 0; i < taille; i++)
            {
                if (Tablo[i] >= pivot)
                {
                    nouveauTablo[index] = Tablo[i];
                    index++;
                }
            }
            Tablo = nouveauTablo;
        }
 
        public static void Main(string[] args) 
        { 
            // Exo 1 : remplir un tableau avec 10 entiers aléatoires compris entre 0 et 20 et afficher ceux au dessus de la moyenne
            int taille = 10; 
            int[] Tablo = new int[taille]; 
            RemlirTab(ref Tablo, taille, 0, 20); 
            AfficheTab(Tablo, taille); 
            int moyenne = FindMoyenne(Tablo, taille); 
            Console.WriteLine("La moyenne vaut " + moyenne); 

            // Exo 3 : Remplissez un tableau avec 10 valeurs entières aléatoires comprises entre 0 et 20.  choisissez  aléatoirement K une valeur pivot contenue dans le tableau.  on déplacera les éléments du tableau de manière à regrouper en tête de celui-ci toutes  les valeurs inférieures à K et en queue, les valeurs supérieures à K. 
            // On tire UNE seule fois un indice au hasard, et on lit la valeur du tableau à cet indice
            int K = Tablo[new Random().Next(0, taille)];
            Console.WriteLine("La valeur pivot est : " + K);
            DeplacerPivot(ref Tablo, K);
            AfficheTab(Tablo, taille);

            // Exo 2 : Remplissez un tableau avec 10 valeurs entières aléatoires comprises entre 0 et 20 et affichez  le contenu de celui-ci. L’utilisateur pourra alors choisir de supprimer une valeur saisie dans le  tableau. Si celle-ci y figure (une ou plusieurs fois) les cases du tableau seront supprimées et le  contenu du tableau modifié affiché. Si le tableau est vide, un message d’erreur dans ce sens  sera affiché.
            Console.WriteLine("Entrez une valeur à supprimer : ");
            int valeurASupprimer = Convert.ToInt32(Console.ReadLine());
            SupprimerValeur(ref Tablo, valeurASupprimer);
            AfficheTab(Tablo, Tablo.Length);
        } 
    } 
}