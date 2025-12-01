using System;
using System.Collections.Generic;

class Exercise3
{
    static void Main()
    {
        Dictionary<string, object> brand = new Dictionary<string, object>();
        brand["name"] = "Zara";
        brand["creation_date"] = 1975;
        brand["creator_name"] = "Amancio Ortega Gaona";
        brand["type_of_clothes"] = new List<string>{"men", "women", "children", "home"};
        brand["international_competitors"] = new List<string>{"Gap", "H&M", "Benetton"};
        brand["number_stores"] = 7000;
        
        Dictionary<string, List<string>> colors = new Dictionary<string, List<string>>();
        colors["France"] = new List<string>{"blue"};
        colors["Spain"] = new List<string>{"red"};
        colors["US"] = new List<string>{"pink", "green"};
        brand["major_color"] = colors;
        
        brand["number_stores"] = 2;
        
        var clothes = (List<string>)brand["type_of_clothes"];
        Console.WriteLine("Zara creates clothes for " + string.Join(", ", clothes) + ".");
        
        brand["country_creation"] = "Spain";
        
        if (brand.ContainsKey("international_competitors"))
        {
            var competitors = (List<string>)brand["international_competitors"];
            competitors.Add("Desigual");
        }
        
        brand.Remove("creation_date");
        
        var comp = (List<string>)brand["international_competitors"];
        Console.WriteLine("Last competitor: " + comp[comp.Count - 1]);
        
        var colors = (Dictionary<string, List<string>>)brand["major_color"];
        var usColors = colors["US"];
        Console.WriteLine("US colors: " + string.Join(", ", usColors));
        
        Console.WriteLine("Number of items: " + brand.Count);
        
        Console.WriteLine("Keys: " + string.Join(", ", brand.Keys));
        
        Dictionary<string, object> more_on_zara = new Dictionary<string, object>();
        more_on_zara["creation_date"] = 1975;
        more_on_zara["number_stores"] = 10000;
        
        foreach (var item in more_on_zara)
        {
            brand[item.Key] = item.Value;
        }
        
        Console.WriteLine("number_stores is now: " + brand["number_stores"]);
        Console.WriteLine("it got replaced because the key was already there");
    }
}
