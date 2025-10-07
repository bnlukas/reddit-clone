using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using RedditClone.Data;
using RedditClone.Models;
namespace RedditClone.Service;

public class DataService
{
    private RedditContent db { get; }
    public DataService(RedditContent db)
    {
        this.db = db;
    }
    public void SeedData()
    {
        if (db.Threads.Any()) return;
        var threads = new List<ThreadsModel>
        {
            new()
            {
                AuthorName = "NoobMaster47",
                Title = "Hvordan bager man drømmekage?",
                ThreadsContent = "",
                Comments = new List<ThreadsCommentsModel>(),
                UpVotes = 4,
                DownVotes = 3,
                Created = DateTime.UtcNow
            },

            new()
            {
                AuthorName = "Benjaminkorteben",
                Title = "Hvordan betaler man moms gennem erhverskonto? ",
                ThreadsContent = "Jeg har lige fået at vide, jeg skal betale moms af dette " +
                                 "xxxxxxx",
                Comments = new List<ThreadsCommentsModel>(),
                UpVotes = 100,
                DownVotes = 238,
                Created = DateTime.UtcNow,
            }

        };   
        db.Threads.AddRange(threads);
        db.SaveChanges();
        Console.WriteLine($"Seed {threads.Count} threads; ");
    }
    public async Task<List<ThreadsModel>> GetAllThreads()
    {
        return await db.Threads
            .Include(t => t.Comments)
            .OrderByDescending(t => t.Created)
            .Take(50)
            .ToListAsync();
    }
    public async Task<ThreadsModel?> GetThreads(int threadsId)
    {
        return await db.Threads
            .Include(t => t.Comments)
            .FirstOrDefaultAsync(t => t.ThreadsId == threadsId);
    }
    public async Task<ThreadsModel> CreateThread(ThreadsModel newThreads)
    {
        db.Threads.Add(newThreads);
        await db.SaveChangesAsync();
        return newThreads;
    }
    public async Task<ThreadsCommentsModel?> AddComment(int threadId, string content, string authorName)
    {
        var thread = await db.Threads
            .Include(t => t.Comments)
            .FirstOrDefaultAsync(t => t.ThreadsId == threadId);
        if (thread == null)
            return null;
        var comment = new ThreadsCommentsModel
        {
            Content = content,
            AuthorName = authorName,
            Created = DateTime.UtcNow,
            ThreadsId = threadId,
        };
        thread.Comments.Add(comment);
        await db.SaveChangesAsync();
        return (comment);
    }
    public enum VoteType
    {
        Up, Down 
    }
    //____ vote on thread 
    public async Task<bool> VoteThread(int threadId, VoteType voteType)
    {
        var thread = await db.Threads
            .FirstOrDefaultAsync(t => t.ThreadsId == threadId);
        if (thread == null) return false;
        if (voteType == VoteType.Up)
            thread.UpVotes++;
        else
            thread.DownVotes++; 
            
        await db.SaveChangesAsync();
        return true;
    }
    //_____ vote on comment 
    public async Task<bool> VoteComment(int commentId, VoteType voteType)
    {
        var comment = await db.ThreadsComments
            .FirstOrDefaultAsync(c => c.CommentId == commentId);
        if (comment == null) return false;
        if (voteType == VoteType.Up)
            comment.UpVotes++;
        else
            comment.DownVotes++;
        await db.SaveChangesAsync();
        return true;
    }
}
