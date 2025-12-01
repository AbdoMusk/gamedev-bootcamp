using System;

class Dog
{
    public string name;
    public int height;
    
    public Dog(string dogName, int dogHeight)
    {
        name = dogName;
        height = dogHeight;
    }
    
    public void Bark()
    {
        Console.WriteLine(name + " goes woof!");
    }
    
    public void Jump()
    {
        int jumpHeight = height * 2;
        Console.WriteLine(name + " jumps " + jumpHeight + " cm high!");
    }
}

class Exercise10
{
    static void Main()
    {
        Dog davidsDog = new Dog("lkalb", 25);
        davidsDog.Bark();
        davidsDog.Jump();
        
        Dog sarahsDog = new Dog("kanich", 15);
        sarahsDog.Bark();
        sarahsDog.Jump();
        
        if (davidsDog.height > sarahsDog.height)
        {
            Console.WriteLine(davidsDog.name + " is taller");
        }
        else
        {
            Console.WriteLine(sarahsDog.name + " is taller");
        }
    }
}
