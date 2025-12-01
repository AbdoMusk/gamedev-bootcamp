using System;
using System.Collections.Generic;

class Exercise2
{
    static void Main()
    {
        Dictionary<string, int> family = new Dictionary<string, int>();
        family["rick"] = 43;
        family["beth"] = 13;
        family["morty"] = 5;
        family["summer"] = 8;
        
        int total = 0;
        
        foreach (var person in family)
        {
            int price = 0;
            
            if (person.Value < 3)
            {
                price = 0;
            }
            else if (person.Value >= 3 && person.Value <= 12)
            {
                price = 10;
            }
            else
            {
                price = 15;
            }
            
            Console.WriteLine(person.Key + ": $" + price);
            total = total + price;
        }
        
        Console.WriteLine("Total: $" + total);
        
        Console.WriteLine("");
        Console.WriteLine("Bonus:");
        Dictionary<string, int> myFamily = new Dictionary<string, int>();
        
        Console.Write("How many people? ");
        int num = int.Parse(Console.ReadLine());
        
        for (int i = 0; i < num; i++)
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Age: ");
            int age = int.Parse(Console.ReadLine());
            myFamily[name] = age;
        }
        
        int total2 = 0;
        foreach (var person in myFamily)
        {
            int price = 0;
            if (person.Value < 3)
                price = 0;
            else if (person.Value >= 3 && person.Value <= 12)
                price = 10;
            else
                price = 15;
            
            Console.WriteLine(person.Key + ": $" + price);
            total2 = total2 + price;
        }
        Console.WriteLine("Total: $" + total2);
    }
}
