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
                break;
            case "4":
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