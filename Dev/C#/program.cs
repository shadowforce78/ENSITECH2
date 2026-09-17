using System;

public class Program{

    static int a = 10;
    static int b = 20;

    public static void Inversion(ref int x, ref int y)
    {
        int temp = x;
        x = y;
        y = temp;
    }

    public static void Main()
    {
        Console.WriteLine("Avant inversion: a = " + a + ", b = " + b);
        Inversion(ref a, ref b);
        Console.WriteLine("Après inversion: a = " + a + ", b = " + b);
    }
}