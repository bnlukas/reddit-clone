using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Model;
using RedditClone.Data;
namespace RedditClone.Service;

public class DataService
{
    private readonly RedditContent db; 
    public DataService(RedditContent db)
    {
        this.db = db;
    }

    public async Task<Post> CreatePostAsync(Post newPost)
    {
        await db.Posts.AddAsync(newPost);
        await db.SaveChangesAsync();
        return newPost;
    }

    public async Task<Comment> AddCommentAsync(int postId, string content, int userId)
    {
        var post = await db.Posts.FindAsync(postId);
        if (post == null) 
            throw new ArgumentException("Du finder ikke et post her");

        var comment = new Comment(content, userId, postId);
        
        post.Comments?.Add(comment);
        await db.SaveChangesAsync();
        return comment;
    }
    public void SeedData()
    {
        if (db.Posts.Any()) return;

        var posts = new List<Post>
        {
            new()
            {
                User = new User { Username = "NoobMaster47" },
                Title = "Hvordan bager man drømmekage?",
                Content = "Jeg vil gerne lære at bage den perfekte drømmekage!",
                Upvotes = 4,
                Downvotes = 3,
            },
            new()
            {
                User = new User { Username = "Benjaminkorteben" },
                Title = "Hvordan betaler man moms gennem erhverskonto?",
                Content = "Jeg har lige fået at vide, jeg skal betale moms...",
                Upvotes = 100,
                Downvotes = 238,
            }
        };

        db.Posts.AddRange(posts);
        db.SaveChanges();
        Console.WriteLine($"Seeded {posts.Count} posts");
    }


    public async Task<List<Post>> GetAllThreads()
    {
        return await db.Posts
            .Include(t => t.Comments)!
            .ThenInclude(c => c.User)
            .OrderByDescending(t => t.Created)
            .Take(50)
            .ToListAsync();
    }

    public async Task<Post?> GetThreads(int threadsId)
    {
        return await db.Posts
            .Include(t => t.User)
            .Include(t => t.Comments)!
            .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(t => t.Id == threadsId);
    }

    public async Task<Post> CreateThread(Post newPost)
    {
        try
        {
            db.Posts.Add(newPost); 
            await db.SaveChangesAsync();
            return newPost;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fejl ved CreateThread:");
            Console.WriteLine(ex.Message);
            if (ex.InnerException != null)
            {
                Console.WriteLine("Indre fejl:");
                Console.WriteLine(ex.InnerException.Message);
            }
            throw;
        }
    }

    public async Task<Comment?> AddComment(int threadId, string content, string authorName)
    {
        var thread = await db.Posts
            .Include(t => t.Comments)
            .FirstOrDefaultAsync(t => t.Id == threadId);
        if (thread == null) return null;

        var newUser = new User { Username = authorName };
        db.Users.Add(newUser);
        await db.SaveChangesAsync();
        
        var comment = new Comment
        {
            Content = content,
            UserId = newUser.Id,
            Created = DateTime.UtcNow
        };

        thread.Comments?.Add(comment);
        await db.SaveChangesAsync();
        return comment;
    }

    public enum VoteType
    {
        Up,
        Down
    }

    //____ vote on thread 
    public async Task<bool> VoteThread(int threadId, VoteType voteType)
    {
        var thread = await db.Posts
            .FirstOrDefaultAsync(t => t.Id == threadId);
        if (thread == null) return false;
        if (voteType == VoteType.Up)
            thread.Upvotes++;
        else
            thread.Downvotes++;

        await db.SaveChangesAsync();
        return true;
    }

    //_____ vote on comment 
    public async Task<bool> VoteComment(int commentId, VoteType voteType)
    {
        var comment = await db.Comments
            .FirstOrDefaultAsync(c => c.Id == commentId); // Ændr til c.Id
        if (comment == null) return false;

        if (voteType == VoteType.Up)
            comment.Upvotes++;
        else
            comment.Downvotes++;

        await db.SaveChangesAsync();
        return true;
    }
}

