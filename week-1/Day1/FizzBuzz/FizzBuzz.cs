using System;

class FizzBuzzChecker
{
    static void Main()
    {
        Console.Write("Enter a number (1-100): ");
        int number = int.Parse(Console.ReadLine());

        if (number % 3 == 0 && number % 5 == 0)
        {
            Console.WriteLine("FizzBuzz");
        }
        else if (number % 3 == 0)
        {
            Console.WriteLine("Fizz");
        }
        else if (number % 5 == 0)
        {
            Console.WriteLine("Buzz");
        }
        else
        {
            Console.WriteLine(number);
        }
    }
}
    