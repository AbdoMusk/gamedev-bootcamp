using System;

class Exercise7
{
    static void Main()
    {
        Console.Write("Enter season: ");
        string season = Console.ReadLine().ToLower();
        
        int temp = GetRandomTemp(season);
        
        Console.WriteLine("Temperature: " + temp + " degrees");
        
        if (temp < 0)
            Console.WriteLine("Its freezing! wear warm clothes");
        else if (temp < 10)
            Console.WriteLine("Its cold, jacket needed");
        else if (temp < 20)
            Console.WriteLine("Nice weather");
        else if (temp < 30)
            Console.WriteLine("Warm weather!");
        else
            Console.WriteLine("Very hot! stay hydrated");
        
        double tempFloat = GetRandomTempFloat(season);
        Console.WriteLine("Float temp: " + tempFloat);
        
        Console.Write("Enter month (1-12): ");
        int month = int.Parse(Console.ReadLine());
        string s = GetSeason(month);
        Console.WriteLine("Season: " + s);
    }
    
    static int GetRandomTemp(string season)
    {
        Random rnd = new Random();
        int min = -10, max = 40;

        if (season == "winter") { min = -10; max = 16; }
        if (season == "spring") { min = 0; max = 23; }
        if (season == "summer") { min = 16; max = 40; }
        if (season == "autumn") { min = 0; max = 23; }

        return rnd.Next(min, max + 1);
    }
    
    static double GetRandomTempFloat(string season)
    {
        Random rnd = new Random();
        int min = -10, max = 40;

        if (season == "winter") { min = -10; max = 16; }
        if (season == "spring") { min = 0; max = 23; }
        if (season == "summer") { min = 16; max = 40; }
        if (season == "autumn") { min = 0; max = 23; }

        return min + (rnd.NextDouble() * (max - min));
    }
    
    static string GetSeason(int month)
    {
        if (month == 12 || month == 1 || month == 2)
            return "winter";
        else if (month >= 3 && month <= 5)
            return "spring";
        else if (month >= 6 && month <= 8)
            return "summer";
        else
            return "autumn";
    }
}
