using System;
using System.Linq;
using NLog;

var db = new BloggingContext();

while (true)
{
    Console.WriteLine("1. Display all blogs");
    Console.WriteLine("2. Add Blog");
    Console.WriteLine("3. Create Post");
    Console.WriteLine("4. Display Posts");
    Console.WriteLine("Enter your option:");

    var userOption = Console.ReadLine();

    try
    {
        switch (userOption)
        {
            case "1":
                var query = db.Blogs.OrderBy(b => b.Name);
                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }
                break;
            case "2":
                Console.Write("Enter a name for a new Blog: ");
                var name = Console.ReadLine();
                var blog = new Blog { Name = name };
                db.Add(blog);
                db.SaveChanges();
                break;
            case "3":
                Console.Write("Enter the name of the Blog you want to post to: ");
                var blogName = Console.ReadLine();
                var selectedBlog = db.Blogs.FirstOrDefault(b => b.Name == blogName);
                if (selectedBlog == null)
                {
                    Console.WriteLine("Blog not found");
                    break;
                }
                Console.Write("Enter the title of the Post: ");
                var postTitle = Console.ReadLine();
                Console.Write("Enter the content of the Post: ");
                var postContent = Console.ReadLine();
                var newPost = new Post { Title = postTitle, Content = postContent, BlogId = selectedBlog.BlogId };
                db.Add(newPost);
                db.SaveChanges();
                break;
            case "4":
                Console.Write("Enter the name of the Blog you want to view posts from: ");
                var blogToView = Console.ReadLine();
                var chosenBlog = db.Blogs.FirstOrDefault(b => b.Name == blogToView);
                if (chosenBlog == null)
                {
                    Console.WriteLine("Blog not found");
                    break;
                }
                var posts = db.Posts.Where(p => p.BlogId == chosenBlog.BlogId);
                Console.WriteLine($"Posts from {chosenBlog.Name}:");
                foreach (var post in posts)
                {
                    Console.WriteLine($"Title: {post.Title}, Content: {post.Content}");
                }
                break;
            default:
                Console.WriteLine("Invalid option");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}