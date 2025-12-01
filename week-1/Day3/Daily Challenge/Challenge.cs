using System;
using System.Collections.Generic;

class DailyChallenge
{
    static void Main()
    {
        Console.Write("Enter a word: ");
        string word = Console.ReadLine();
        
        Dictionary<char, List<int>> letterPositions = new Dictionary<char, List<int>>();
        
        // go through each letter in the word
        int position = 0;
        foreach (char letter in word)
        {
            if (letterPositions.ContainsKey(letter))
            {
                // letter already exists, add position to list
                letterPositions[letter].Add(position);
            }
            else
            {
                // new letter, create new list
                List<int> positions = new List<int>();
                positions.Add(position);
                letterPositions.Add(letter, positions);
            }
            
            position = position + 1;
        }
        
        // print the results
        Console.WriteLine("");
        Console.WriteLine("Output:");
        foreach (char letter in letterPositions.Keys)
        {
            List<int> positions = letterPositions[letter];
            
            Console.Write(letter + ": ");
            
            int i = 0;
            foreach (int pos in positions)
            {
                Console.Write(pos);
                if (i < positions.Count - 1)
                {
                    Console.Write(", ");
                }
                i = i + 1;
            }
            
            Console.WriteLine("");
        }
    }
}
