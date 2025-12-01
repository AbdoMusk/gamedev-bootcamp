using System;
using System.Collections.Generic;

class Exercise1
{
    static void Main()
    {
        Dictionary<string, string> birthdays = new Dictionary<string, string>();
        
        birthdays.Add("John", "1990/05/15");
        birthdays.Add("Sarah", "1985/12/20");
        birthdays.Add("Mike", "1992/03/08");
        birthdays.Add("Emma", "1988/07/25");
        birthdays.Add("David", "1995/11/30");
        
        Console.WriteLine("Welcome to the Birthday Lookup!");
        Console.WriteLine("");
        Console.WriteLine("You can look up birthdays of people in our database.");
        Console.WriteLine("");
        
        Console.Write("Enter a person's name: ");
        string name = Console.ReadLine();
        
        string birthday = birthdays[name];
        Console.WriteLine("The birthday of " + name + " is: " + birthday);
    }
}
