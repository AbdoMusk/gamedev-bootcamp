using System;

class DistinctCharacterSequence
{
    static void Main()
    {
        Console.Write("Enter a string: ");
        string input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("No input provided.");
            return;
        }

        string result = "";
        char previous = input[0];
        result += previous;

        for (int i = 1; i < input.Length; i++)
        {
            if (input[i] != previous)
            {
                result += input[i];
                previous = input[i];
            }
        }

        Console.WriteLine("Sequence: " + result);
    }
}