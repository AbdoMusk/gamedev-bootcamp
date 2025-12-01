using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Sphere Challenge ===");
        Console.WriteLine("");

        // create some spheres
        Sphere sphere1 = new Sphere(5.0);
        Sphere sphere2 = new Sphere(3.0);
        Sphere sphere3 = new Sphere(7.5);

        Console.WriteLine("--- Created Spheres ---");
        Console.WriteLine(sphere1.ToString());
        Console.WriteLine(sphere2.ToString());
        Console.WriteLine(sphere3.ToString());
        Console.WriteLine("");

        // test diameter
        Console.WriteLine("--- Testing Diameter ---");
        Console.WriteLine("Sphere1 diameter: " + sphere1.GetDiameter());
        sphere2.SetDiameter(10.0);
        Console.WriteLine("Sphere2 after setting diameter to 10: " + sphere2.ToString());
        Console.WriteLine("");

        // add two spheres
        Console.WriteLine("--- Adding Spheres ---");
        Sphere sphere4 = sphere1 + sphere2;
        Console.WriteLine("Sphere1 + Sphere2 = " + sphere4.ToString());
        Console.WriteLine("");

        // compare spheres
        Console.WriteLine("--- Comparing Spheres ---");
        if (sphere1 > sphere2)
        {
            Console.WriteLine("Sphere1 is bigger than Sphere2");
        }
        else
        {
            Console.WriteLine("Sphere2 is bigger than Sphere1");
        }

        if (sphere3 > sphere1)
        {
            Console.WriteLine("Sphere3 is bigger than Sphere1");
        }
        else
        {
            Console.WriteLine("Sphere1 is bigger than Sphere3");
        }
        Console.WriteLine("");

        // check equality
        Console.WriteLine("--- Testing Equality ---");
        Sphere sphere5 = new Sphere(5.0);
        if (sphere1 == sphere5)
        {
            Console.WriteLine("Sphere1 and Sphere5 are equal");
        }
        else
        {
            Console.WriteLine("Sphere1 and Sphere5 are not equal");
        }

        if (sphere1 != sphere2)
        {
            Console.WriteLine("Sphere1 and Sphere2 are not equal");
        }
        Console.WriteLine("");

        // create a list of spheres and sort them
        Console.WriteLine("--- Sorting Spheres ---");
        List<Sphere> spheres = new List<Sphere>();
        spheres.Add(new Sphere(8.0));
        spheres.Add(new Sphere(2.0));
        spheres.Add(new Sphere(5.5));
        spheres.Add(new Sphere(1.0));
        spheres.Add(new Sphere(10.0));

        Console.WriteLine("Before sorting:");
        int i = 0;
        while (i < spheres.Count)
        {
            Console.WriteLine("Sphere " + (i + 1) + ": " + spheres[i].ToString());
            i = i + 1;
        }
        Console.WriteLine("");

        // mistake: using basic Sort without proper comparison delegate syntax
        spheres.Sort(SphereComparer.CompareByRadius);
        
        Console.WriteLine("After sorting by radius:");
        i = 0;
        while (i < spheres.Count)
        {
            Console.WriteLine("Sphere " + (i + 1) + ": " + spheres[i].ToString());
            i = i + 1;
        }
        Console.WriteLine("");

        // sort by volume
        spheres.Sort(SphereComparer.CompareByVolume);
        
        Console.WriteLine("After sorting by volume:");
        i = 0;
        while (i < spheres.Count)
        {
            Console.WriteLine("Sphere " + (i + 1) + ": " + spheres[i].ToString());
            i = i + 1;
        }
        Console.WriteLine("");

        Console.WriteLine("=== End of Demo ===");
    }
}
