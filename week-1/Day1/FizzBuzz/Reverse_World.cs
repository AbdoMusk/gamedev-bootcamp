using System;

class StringReverser
{
    static void Main()
    {
        Console.Write("Enter text to reverse: ");
        string input = Console.ReadLine();

        string reversed = "";
        for (int i = input.Length - 1; i >= 0; i--)
        {
            reversed += input[i];
        }

        Console.WriteLine("Reversed: " + reversed);
    }
}