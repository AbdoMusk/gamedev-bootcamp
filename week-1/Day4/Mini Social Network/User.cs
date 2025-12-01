using System;
using System.Collections.Generic;

class User
{
    public string Name;
    public int Age;
    public List<User> Friends;
    public List<Post> Posts;

    public User(string name, int age)
    {
        Name = name;
        Age = age;
        Friends = new List<User>();
        Posts = new List<Post>();
    }

    public void AddFriend(User user)
    {
        // check if trying to add yourself
        if (user == this)
        {
            Console.WriteLine("You cannot add yourself as a friend.");
            return;
        }

        // check if already friends
        int i = 0;
        while (i < Friends.Count)
        {
            if (Friends[i] == user)
            {
                Console.WriteLine(user.Name + " is already your friend.");
                return;
            }
            i = i + 1;
        }

        // add the friend
        Friends.Add(user);
        Console.WriteLine(user.Name + " has been added as a friend.");
    }

    public void RemoveFriend(User user)
    {
        // go through friends list to find and remove
        int i = 0;
        while (i < Friends.Count)
        {
            if (Friends[i] == user)
            {
                Friends.RemoveAt(i);
                Console.WriteLine(user.Name + " has been removed from your friends.");
                return;
            }
            i = i + 1;
        }

        Console.WriteLine(user.Name + " is not in your friends list.");
    }

    public void ShowFeed()
    {
        Console.WriteLine("=== " + Name + "'s News Feed ===");
        Console.WriteLine("");

        // collect all posts from friends
        List<Post> allPosts = new List<Post>();
        
        int i = 0;
        while (i < Friends.Count)
        {
            User friend = Friends[i];
            int j = 0;
            while (j < friend.Posts.Count)
            {
                allPosts.Add(friend.Posts[j]);
                j = j + 1;
            }
            i = i + 1;
        }

        // show each post
        if (allPosts.Count == 0)
        {
            Console.WriteLine("No posts to show. Add some friends or wait for them to post!");
        }
        else
        {
            int postNum = 0;
            while (postNum < allPosts.Count)
            {
                Post post = allPosts[postNum];
                Console.WriteLine(post.Author.Name + " posted: \"" + post.Content + "\" (Likes: " + post.Likes.Count + ")");
                
                // show comments
                int k = 0;
                while (k < post.Comments.Count)
                {
                    Comment comment = post.Comments[k];
                    Console.WriteLine("  " + comment.Author.Name + " commented: \"" + comment.Content + "\" (Likes: " + comment.Likes.Count + ")");
                    k = k + 1;
                }
                
                Console.WriteLine("");
                postNum = postNum + 1;
            }
        }
    }
}
