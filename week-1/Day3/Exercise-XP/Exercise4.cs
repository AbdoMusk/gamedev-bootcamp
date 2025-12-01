using System;

class Exercise4
{
    static void Main()
    {
        DescribeCity("Reykjavik");
        DescribeCity("Oslo", "Norway");
        DescribeCity("Paris", "France");
    }
    
    static void DescribeCity(string city, string country = "Iceland")
    {
        Console.WriteLine(city + " is in " + country + ".");
    }
}
