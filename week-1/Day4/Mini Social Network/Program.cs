using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Mini Social Network ===");
        Console.WriteLine("");

        // create users
        User alice = new User("Alice", 25);
        User bob = new User("Bob", 30);
        User carol = new User("Carol", 22);
        User dave = new User("Dave", 28);

        Console.WriteLine("--- Adding Friends ---");
        // alice adds bob and carol as friends
        alice.AddFriend(bob);
        alice.AddFriend(carol);
        
        // bob adds alice and carol
        bob.AddFriend(alice);
        bob.AddFriend(carol);
        bob.AddFriend(dave);
        
        // carol adds alice
        carol.AddFriend(alice);
        
        // dave adds bob
        dave.AddFriend(bob);
        
        Console.WriteLine("");

        // try to add duplicate friend
        Console.WriteLine("--- Testing Duplicate Friend ---");
        alice.AddFriend(bob);
        Console.WriteLine("");

        // try to add yourself
        Console.WriteLine("--- Testing Add Self ---");
        alice.AddFriend(alice);
        Console.WriteLine("");

        Console.WriteLine("--- Creating Posts ---");
        // create some posts
        Post post1 = new Post(alice, "Hello world! This is my first post!");
        alice.Posts.Add(post1);
        Console.WriteLine("Alice created a post.");

        Post post2 = new Post(bob, "Having a great day today!");
        bob.Posts.Add(post2);
        Console.WriteLine("Bob created a post.");

        Post post3 = new Post(carol, "Anyone want to grab coffee?");
        carol.Posts.Add(post3);
        Console.WriteLine("Carol created a post.");

        Post post4 = new Post(dave, "Just finished a coding project!");
        dave.Posts.Add(post4);
        Console.WriteLine("Dave created a post.");

        Console.WriteLine("");

        Console.WriteLine("--- Adding Comments ---");
        // create comments
        Comment comment1 = new Comment(bob, "Nice post!");
        post1.AddComment(comment1);

        Comment comment2 = new Comment(carol, "Welcome to the network!");
        post1.AddComment(comment2);

        Comment comment3 = new Comment(alice, "Thanks Bob!");
        post2.AddComment(comment3);

        Comment comment4 = new Comment(alice, "I'd love to!");
        post3.AddComment(comment4);

        Comment comment5 = new Comment(bob, "Awesome work!");
        post4.AddComment(comment5);

        Console.WriteLine("");

        Console.WriteLine("--- Adding Likes to Posts ---");
        // like some posts
        post1.AddLike(bob);
        post1.AddLike(carol);
        post1.AddLike(dave);
        
        post2.AddLike(alice);
        post2.AddLike(carol);
        
        post3.AddLike(alice);
        post3.AddLike(bob);
        
        post4.AddLike(bob);
        
        Console.WriteLine("");

        // test duplicate like
        Console.WriteLine("--- Testing Duplicate Like ---");
        post1.AddLike(bob);
        Console.WriteLine("");

        Console.WriteLine("--- Adding Likes to Comments ---");
        // like some comments
        comment1.AddLike(alice);
        comment1.AddLike(carol);
        
        comment2.AddLike(alice);
        
        comment3.AddLike(bob);
        
        comment4.AddLike(carol);
        comment4.AddLike(bob);
        
        Console.WriteLine("");

        Console.WriteLine("--- Showing News Feeds ---");
        Console.WriteLine("");
        
        // show alice's feed (should see bob and carol's posts)
        alice.ShowFeed();
        
        // show bob's feed (should see alice, carol, and dave's posts)
        bob.ShowFeed();
        
        // show carol's feed (should only see alice's posts)
        carol.ShowFeed();
        
        // show dave's feed (should see bob's posts)
        dave.ShowFeed();

        Console.WriteLine("--- Removing a Friend ---");
        alice.RemoveFriend(carol);
        Console.WriteLine("");
        
        Console.WriteLine("Alice's updated feed:");
        alice.ShowFeed();

        Console.WriteLine("=== End of Demo ===");
    }
}
