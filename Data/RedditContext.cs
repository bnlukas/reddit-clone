using Microsoft.EntityFrameworkCore;
using Model;


namespace RedditClone.Data;

public class RedditContent : DbContext  
{
    
    private string DbPath { get; }
    public DbSet<Post> Posts { get; set; } = null!; 
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!; 
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId);
    }
}



