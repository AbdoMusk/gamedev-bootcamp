using System;
using System.Collections.Generic;

class Comment
{
    public User Author;
    public string Content;
    public List<User> Likes;

    public Comment(User author, string content)
    {
        Author = author;
        Content = content;
        Likes = new List<User>();
    }

    public void AddLike(User user)
    {
        // check if user already liked this comment
        int i = 0;
        while (i < Likes.Count)
        {
            if (Likes[i] == user)
            {
                Console.WriteLine(user.Name + " already liked this comment.");
                return;
            }
            i = i + 1;
        }

        Likes.Add(user);
        Console.WriteLine(user.Name + " liked the comment.");
    }
}
