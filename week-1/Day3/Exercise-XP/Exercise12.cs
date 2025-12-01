using System;
using System.Collections.Generic;

class Zoo
{
    public string name;
    public List<string> animals;
    public Dictionary<string, List<string>> groups;
    
    public Zoo(string zooName)
    {
        name = zooName;
        animals = new List<string>();
        groups = new Dictionary<string, List<string>>();
    }
    
    public void AddAnimal(string newAnimal)
    {
        if (!animals.Contains(newAnimal))
        {
            animals.Add(newAnimal);
            Console.WriteLine("Added " + newAnimal);
        }
        else
        {
            Console.WriteLine(newAnimal + " already exists");
        }
    }
    
    public void GetAnimals()
    {
        Console.WriteLine("Animals in " + name + ":");
        foreach (string animal in animals)
        {
            Console.WriteLine("- " + animal);
        }
    }
    
    public void SellAnimal(string animalSold)
    {
        if (animals.Contains(animalSold))
        {
            animals.Remove(animalSold);
            Console.WriteLine("Sold " + animalSold);
        }
        else
        {
            Console.WriteLine(animalSold + " not found");
        }
    }
    
    public void SortAnimals()
    {
        animals.Sort();
        
        foreach (string animal in animals)
        {
            string firstLetter = animal[0].ToString().ToUpper();
            
            if (!groups.ContainsKey(firstLetter))
            {
                groups[firstLetter] = new List<string>();
            }
            
            groups[firstLetter].Add(animal);
        }
    }
    
    public void GetGroups()
    {
        Console.WriteLine("Animal groups:");
        foreach (var group in groups)
        {
            Console.WriteLine(group.Key + ": " + string.Join(", ", group.Value));
        }
    }
}

class Exercise12
{
    static void Main()
    {
        Zoo ramatGan = new Zoo("Ramat Gan Safari");
        
        ramatGan.AddAnimal("Giraffe");
        ramatGan.AddAnimal("Ape");
        ramatGan.AddAnimal("Baboon");
        ramatGan.AddAnimal("Bear");
        ramatGan.AddAnimal("Cat");
        ramatGan.AddAnimal("Cougar");
        ramatGan.AddAnimal("Eel");
        ramatGan.AddAnimal("Emu");
        
        ramatGan.GetAnimals();
        
        ramatGan.SellAnimal("Bear");
        ramatGan.GetAnimals();
        
        ramatGan.SortAnimals();
        ramatGan.GetGroups();
    }
}
