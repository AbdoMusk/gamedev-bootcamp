using System;
using System.Collections.Generic;

class Post
{
    public User Author;
    public string Content;
    public List<Comment> Comments;
    public List<User> Likes;

    public Post(User author, string content)
    {
        Author = author;
        Content = content;
        Comments = new List<Comment>();
        Likes = new List<User>();
    }

    public void AddComment(Comment comment)
    {
        // check if comment is empty
        if (comment.Content == "" || comment.Content == null)
        {
            Console.WriteLine("Cannot add empty comment.");
            return;
        }

        Comments.Add(comment);
        Console.WriteLine(comment.Author.Name + " added a comment.");
    }

    public void AddLike(User user)
    {
        // check if user already liked this post
        int i = 0;
        while (i < Likes.Count)
        {
            if (Likes[i] == user)
            {
                Console.WriteLine(user.Name + " already liked this post.");
                return;
            }
            i = i + 1;
        }

        Likes.Add(user);
        Console.WriteLine(user.Name + " liked the post.");
    }
}
