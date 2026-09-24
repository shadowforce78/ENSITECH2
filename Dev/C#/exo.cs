using System;

public class Program{

    public static int Add(int a, int b){
        return a + b;
    }

    public static String AfficherCharactere(char c, int n){
        String result = "";
        for(int i = 0; i < n; i++){
            result += c;
        }
        return result;
    }

    public static String AfficherCaractereEtSeparateur(char c, int n, char sep){
        String result = "";
        for(int i = 0; i < n; i++){
            result += c;
            if(i < n - 1){
                result += sep;
            }
        }
        return result;
    }

    public static int RetournePlusGrand(int a, int b, int c){
        if(a > b && a > c){
            return a;
        } else if(b > a && b > c){
            return b;
        } else {
            return c;
        }
    }

    public static void Main(){
        int result = Add(10, 20);
        String result2 = AfficherCharactere('A', 5);
        String result3 = AfficherCaractereEtSeparateur('B', 4, '-');
        int max = RetournePlusGrand(10, 20, 15);

        Console.WriteLine("10 + 20 = " + result);
        Console.WriteLine("A x 5 = " + result2);
        Console.WriteLine("B x 4 avec séparateur = " + result3);
        Console.WriteLine("Le plus grand des trois nombres (10, 20, 15) est : " + max);
    }
}