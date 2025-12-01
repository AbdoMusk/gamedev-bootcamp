using System;
using System.Collections.Generic;

class Song
{
    public List<string> lyrics;
    
    public Song(List<string> songLyrics)
    {
        lyrics = songLyrics;
    }
    
    public void SingMeASong()
    {
        foreach (string line in lyrics)
        {
            Console.WriteLine(line);
        }
    }
}

class Exercise11
{
    static void Main()
    {
        List<string> lyrics = new List<string>();
        lyrics.Add("There's a lady who's sure");
        lyrics.Add("all that glitters is gold");
        lyrics.Add("and she's buying a stairway to heaven");
        
        Song stairway = new Song(lyrics);
        stairway.SingMeASong();
    }
}
