using Microsoft.EntityFrameworkCore;
using RedditClone.Models;

namespace RedditClone.Data;

public class RedditContent : DbContext  
{
    public DbSet<ThreadsModel> Threads { get; set; }
 
    public DbSet<ThreadsCommentsModel> ThreadsComments { get; set; }
    
    
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



