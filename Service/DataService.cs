using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
    
    //_______ Mock data + fylder db hvis tom
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

    //___ __ Henter alle threads med kommentare og brugere
    public async Task<List<Post>> GetAllThreads()
    {
        return await db.Posts
            .Include(t => t.Comments)!
            .ThenInclude(c => c.User)
            .OrderByDescending(t => t.Created)
            .Take(50)
            .ToListAsync();
    }

    //______ HENTER EN specifik post med detaljer
    public async Task<Post?> GetThreads(int threadsId)
    {
        return await db.Posts
            .Include(t => t.User)
            .Include(t => t.Comments)!
            .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(t => t.Id == threadsId);
    }

    
    //______ Opretter en ny post i db
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
            // en smule fejl loggin til debuggin 
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

    
    //______ tilføjer kommentar til en post
    public async Task<Comment?> AddComment(int threadId, string content, string authorName)
    {
        //___ finder posten
        var thread = await db.Posts
            .Include(t => t.Comments)
            .FirstOrDefaultAsync(t => t.Id == threadId);
        if (thread == null) return null;

        //___ ny kommentar, ny user
        var newUser = new User { Username = authorName };
        db.Users.Add(newUser);
        await db.SaveChangesAsync();
        
        //___ new comment
        var comment = new Comment
        {
            Content = content,
            UserId = newUser.Id,
            Created = DateTime.UtcNow
        };
        
        //___ tilføjer kommentaren til postens liste
        thread.Comments?.Add(comment);
        await db.SaveChangesAsync();
        return comment;
    }

    
    //__________ Til VOTETYPE UP N DOWN 
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

    //______ Sortering og flitering 
    public async Task<List<Post>> GetThreadsSorted(string sortBy = "newest", string filterBy = "all")
    {
        var query = db.Posts
            .Include(t => t.Comments)
            .ThenInclude(c => c.User)
            .AsQueryable();

        
        //_____ flitering 
        if (filterBy == "popular")
        {
            query = query
                .Where(p => (p.Upvotes - p.Downvotes) >= 10); 
        }
        
        //___ kun posts hvor folk er uengie (up n downvotes)
        else if (filterBy == "controversial")
        {
            query = query
                .Where(p => (p.Upvotes > 0 && p.Downvotes > 0)); 
            
        }
        
        //____ SORTTERING 
        query = sortBy.ToLower() switch
        {
            "votes" => query
                .OrderByDescending(p => (p.Upvotes - p.Downvotes)),

            "comments" => query
                .OrderByDescending(p => p.Comments.Count),

            "oldest" => query
                .OrderBy(p => p.Created),
            _ => query
                .OrderByDescending(p => p.Created),

        };
        return await query.Take(50).ToListAsync();
    }
}

