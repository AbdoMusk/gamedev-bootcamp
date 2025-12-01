using System;
using System.Collections.Generic;

class Exercise2
{
    static void Main()
    {
        Dictionary<string, string> birthdays = new Dictionary<string, string>();
        
        birthdays.Add("John", "1990/05/15");
        birthdays.Add("Sarah", "1985/12/20");
        birthdays.Add("Mike", "1992/03/08");
        birthdays.Add("Emma", "1988/07/25");
        birthdays.Add("David", "1995/11/30");
        
        Console.WriteLine("People in our database:");
        Console.WriteLine("");
        
        foreach (string name in birthdays.Keys)
        {
            Console.WriteLine(name);
        }
        
        Console.WriteLine("");
        Console.Write("Enter a person's name: ");
        string input = Console.ReadLine();
        
        if (birthdays.ContainsKey(input))
        {
            string birthday = birthdays[input];
            Console.WriteLine("The birthday of " + input + " is: " + birthday);
        }
        else
        {
            Console.WriteLine("Sorry, we don't have the birthday information for " + input);
        }
    }
}
