using System;

class MultiplesGenerator
{
    static void Main()
    {
        Console.Write("Enter a number: ");
        int number = int.Parse(Console.ReadLine());

        Console.Write("How many multiples? ");
        int count = int.Parse(Console.ReadLine());

        int[] results = new int[count];
        for (int i = 0; i < count; i++)
        {
            results[i] = number * (i + 1);
        }

        Console.WriteLine("Resulting multiples:");
        for (int i = 0; i < results.Length; i++)
        {
            if (i > 0) Console.Write(", ");
            Console.Write(results[i]);
        }
        Console.WriteLine();
    }
}
