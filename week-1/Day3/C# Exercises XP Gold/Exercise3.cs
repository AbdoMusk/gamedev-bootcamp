using System;

class Exercise3
{
    static int CalculateSum(int x)
    {
        int firstNumber = x;
        int secondNumber = x * 10 + x;
        int thirdNumber = x * 100 + x * 10 + x;
        int fourthNumber = x * 1000 + x * 100 + x * 10 + x;
        
        int total = firstNumber + secondNumber + thirdNumber + fourthNumber;
        
        return total;
    }
    
    static void Main()
    {
        Console.Write("Enter a number: ");
        string input = Console.ReadLine();
        int x = int.Parse(input);
        
        int result = CalculateSum(x);
        
        Console.WriteLine("The result is: " + result);
    }
}
