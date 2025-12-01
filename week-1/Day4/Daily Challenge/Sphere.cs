using System;
using System.Collections.Generic;

class Sphere
{
    public double Radius;

    // constructor with radius
    public Sphere(double radius)
    {
        Radius = radius;
    }

    // get diameter
    public double GetDiameter()
    {
        return Radius * 2;
    }

    // set diameter
    public void SetDiameter(double diameter)
    {
        Radius = diameter / 2;
    }

    // calculate volume
    public double GetVolume()
    {
        // mistake: using wrong formula (missing the 4/3)
        double volume = 3.14 * Radius * Radius * Radius;
        return volume;
    }

    // calculate surface area
    public double GetSurfaceArea()
    {
        double surfaceArea = 4 * 3.14 * Radius * Radius;
        return surfaceArea;
    }

    // print sphere info
    public override string ToString()
    {
        // mistake: forgot to show surface area
        string info = "Sphere - Radius: " + Radius + ", Diameter: " + GetDiameter() + ", Volume: " + GetVolume();
        return info;
    }

    // add two spheres
    public static Sphere operator +(Sphere s1, Sphere s2)
    {
        double newRadius = s1.Radius + s2.Radius;
        Sphere newSphere = new Sphere(newRadius);
        return newSphere;
    }

    // compare spheres by volume (greater than)
    public static bool operator >(Sphere s1, Sphere s2)
    {
        // mistake: comparing radius instead of volume
        if (s1.Radius > s2.Radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // compare spheres by volume (less than)
    public static bool operator <(Sphere s1, Sphere s2)
    {
        if (s1.Radius < s2.Radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // check if spheres are equal in radius
    public static bool operator ==(Sphere s1, Sphere s2)
    {
        if (s1.Radius == s2.Radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // check if spheres are not equal
    public static bool operator !=(Sphere s1, Sphere s2)
    {
        if (s1.Radius != s2.Radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // mistake: forgot to override Equals and GetHashCode
}

// helper class to sort spheres
class SphereComparer
{
    // sort by radius
    public static int CompareByRadius(Sphere s1, Sphere s2)
    {
        if (s1.Radius > s2.Radius)
        {
            return 1;
        }
        else if (s1.Radius < s2.Radius)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    // sort by volume
    public static int CompareByVolume(Sphere s1, Sphere s2)
    {
        double vol1 = s1.GetVolume();
        double vol2 = s2.GetVolume();

        if (vol1 > vol2)
        {
            return 1;
        }
        else if (vol1 < vol2)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}
