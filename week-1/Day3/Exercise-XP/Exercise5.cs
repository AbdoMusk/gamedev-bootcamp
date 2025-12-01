using System;

class Exercise5
{
    static void Main()
    {
        Console.Write("Enter a number 1-100: ");
        int myNum = int.Parse(Console.ReadLine());
        
        Random rnd = new Random();
        int randomNum = rnd.Next(1, 101);
        
        if (myNum == randomNum)
        {
            Console.WriteLine("Success!");
        }
        else
        {
            Console.WriteLine("Failed.");
        }
        Console.WriteLine("Your number: " + myNum);
        Console.WriteLine("Random number: " + randomNum);
    }
}
