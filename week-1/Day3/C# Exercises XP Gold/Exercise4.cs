using System;
using System.Collections.Generic;

class Exercise4
{
    static Random random = new Random();
    
    static int ThrowDice()
    {
        int result = random.Next(1, 7);
        return result;
    }
    
    static int ThrowUntilDoubles()
    {
        int throwCount = 0;
        
        while (true)
        {
            int dice1 = ThrowDice();
            int dice2 = ThrowDice();
            throwCount = throwCount + 1;
            
            if (dice1 == dice2)
            {
                break;
            }
        }
        
        return throwCount;
    }
    
    static void MainSimulation()
    {
        List<int> results = new List<int>();
        
        int i = 0;
        while (i < 100)
        {
            int throws = ThrowUntilDoubles();
            results.Add(throws);
            i = i + 1;
        }
        
        int totalThrows = 0;
        foreach (int throws in results)
        {
            totalThrows = totalThrows + throws;
        }
        
        double average = (double)totalThrows / results.Count;
        double roundedAverage = Math.Round(average, 2);
        
        Console.WriteLine("Total number of throws: " + totalThrows);
        Console.WriteLine("Average throws to get doubles: " + roundedAverage);
    }
    
    static void Main()
    {
        MainSimulation();
    }
}
