using System;

class Exercise6
{
    static void Main()
    {
        MakeShirt();
        MakeShirt("Medium");
        MakeShirt("Small", "Game Dev!");
        
        MakeShirt(text: "Hello World", size: "XL");
    }
    
    static void MakeShirt(string size = "Large", string text = "I love C#")
    {
        Console.WriteLine("The size of the shirt is " + size + " and the text is '" + text + "'.");
    }
}
