using Microsoft.EntityFrameworkCore;
using Model;


namespace RedditClone.Data;

public class RedditContent : DbContext  
{
    private string DbPath { get; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}"); 
            
    }
    public RedditContent()
    {
        var folder = AppContext.BaseDirectory; 
        DbPath = Path.Combine(folder, "reddit.db");
        Console.WriteLine($"Db vil være her {DbPath}"); 
    }
}



