using System;

class Cat
{
    public string name;
    public int age;
    
    public Cat(string catName, int catAge)
    {
        name = catName;
        age = catAge;
    }
}

class Exercise9
{
    static void Main()
    {
        Cat cat1 = new Cat("mcha", 2);
        Cat cat2 = new Cat("l9atta", 5);
        Cat cat3 = new Cat("mouch", 4);
        
        Cat oldest = FindOldest(cat1, cat2, cat3);
        
        Console.WriteLine("The oldest cat is " + oldest.name + ", and is " + oldest.age + " years old.");
    }
    
    static Cat FindOldest(Cat c1, Cat c2, Cat c3)
    {
        if (c1.age > c2.age && c1.age > c3.age)
            return c1;
        else if (c2.age > c3.age)
            return c2;
        else
            return c3;
    }
}
