using System;
using System.Collections.Generic;

class Exercise1
{
    static void Main()
    {
        List<string> keys = new List<string> { "Ten", "Twenty", "Thirty" };
        List<int> values = new List<int> { 10, 20, 30 };
        
        Dictionary<string, int> result = new Dictionary<string, int>();
        
        for (int i = 0; i < keys.Count; i++)
        {
            result[keys[i]] = values[i];
        }
        
        foreach (var item in result)
        {
            Console.WriteLine(item.Key + ": " + item.Value);
        }
    }
}
