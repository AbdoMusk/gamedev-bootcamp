## <span style="color:#87CEEB; font-weight:bold;">Practical Exercises C#</span>

**- Exercise 1: <span style="color:#FFD700;">The Greeting</span> Task :**
```
using System;

class GreetingApp
{
    static void Main()
    {
        Console.WriteLine("Hello! Enjoy learning C#.");
    }
}
```
**- Exercise 2: <span style="color:#FFD700;">Personal Info</span> Task :**
```
using System;

class PersonalInfo
{
    static void Main()
    {
        string name = "abdellah";
        int age = 28;
        string city = "marrakech";

        Console.WriteLine("My name is " + name + ", I am " + age + " years old and I live in " + city + ".");
    }
}
```

**- Exercise 3: <span style="color:#FFD700;">The Calculator</span> Task :**
```
using System;

class SimpleCalculator
{
    static void Main()
    {
        int first = 10;
        int second = 5;
        int total = first + second;

        Console.WriteLine("Sum: " + total);
    }
}
```

**- Exercise 4: <span style="color:#FFD700;">The Bouncer (If/Else)</span> Task :**
```
using System;

class BouncerCheck
{
    static void Main()
    {
        int age = 16;

        if (age >= 18)
        {
            Console.WriteLine("Allowed in.");
        }
        else
        {
            Console.WriteLine("Not allowed.");
        }
    }
}
```

**- Exercise 5: <span style="color:#FFD700;">The Countdown (Loops)</span>Task :**
```
using System;

class CountdownApp
{
    static void Main()
    {
        int n = 10;

        while (n > 0)
        {
            Console.WriteLine(n);
            n--;
        }

        Console.WriteLine("Go!");
    }
}
```

**- Exercise 6: <span style="color:#FFD700;">The Repeater (Functions)</span> Task :**
```
using System;

class MessageRepeater
{
    static void Main()
    {
        PrintHello("Alex");
        PrintHello("Sam");
        PrintHello("Taylor");
    }

    static void PrintHello(string name)
    {
        Console.WriteLine("Hello, " + name + "!");
    }
}
```

**- Exercise 7: <span style="color:#FFD700;">Even or Odd?</span> Task :**
```
using System;

class EvenOddChecker
{
    static void Main()
    {
        for (int i = 1; i <= 10; i++)
        {
            if (i % 2 == 0)
            {
                Console.WriteLine(i + " is Even");
            }
            else
            {
                Console.WriteLine(i + " is Odd");
            }
        }
    }
}
```

**- Exercise 8: <span style="color:#FFD700;">Temperature Converter</span> Task :**
```
using System;

class TemperatureConverter
{
    static void Main()
    {
        Console.Write("Enter temperature in Celsius: ");
        double celsius = Convert.ToDouble(Console.ReadLine());
        
        double fahrenheit = celsius * 9 / 5 + 32;
        
        Console.WriteLine(celsius+ "°C is equal to "+ fahrenheit+ "°F");
    }
}
```

**- Exercise 9: <span style="color:#FFD700;">Number Swapper</span> Task :**
```
using System;

class NumberSwapper
{
    static void Main()
    {
        int a = 10;
        int b = 20;
        
        Console.WriteLine("Before swap: a=" + a + ", b =" + b);
        
        int temp = a;
        a = b;
        b = temp;
        
        Console.WriteLine("After swap : a =" + a + ", b = " + b);
    }
}
```

**- Exercise 10: <span style="color:#FFD700;">Simple Multiplication Table</span> Task :**
```
using System;

class MultiplicationTable
{
    static void Main()
    {
        Console.Write("Enter a number: ");
        int number = Convert.ToInt32(Console.ReadLine());
        
        Console.WriteLine("Multiplication table for " + number + ":");
        
        for (int i = 1; i <= 10; i++)
        {
            Console.WriteLine(number +" x " + i +" = " + (number * i));
        }
    }
}
```