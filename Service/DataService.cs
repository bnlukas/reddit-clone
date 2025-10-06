using Microsoft.EntityFrameworkCore;
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
        ThreadsModel? threads = db.Threads.FirstOrDefault()!;
        if (threads == null)
        {
            threads = new ThreadsModel
            {
                ThreadsId = 1,
                AuthorName = "NoobMaster47",
                Title = "Hvordan bager man drømmekage?",
                ThreadsContent = "",
                Comments = new List<ThreadsCommentsModel>(),
                Upvotes = 4,
                Downvotes = 3,
                Created = DateTime.UtcNow,
                
            };
        }
        db.SaveChanges();
    }


    public List<ThreadsModel> GetAllThreads()
    {
        return db.Threads
            .Include(t => t.Comments)
            .OrderByDescending(t => t.Created)
            .Take(50)
            .ToList();
    }

    public ThreadsModel? GetThreads(int threadsId)
    {
        return db.Threads
            .Include(t => t.Comments)
            .FirstOrDefault(t => t.ThreadsId == threadsId);
    }

    public ThreadsModel CreateThread(ThreadsModel newThreads)
    {
        db.Threads.Add(newThreads);
        db.SaveChanges();
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
            Comments = content,
            AuthorName = authorName,
            Created = DateTime.UtcNow,
            PostId = threadId

        };
        thread.Comments.Add(comment);
        await db.SaveChangesAsync();
        return (comment);
    }


    public async Task<bool> VoteThread(int threadId)
    {
        var thread = await db.Threads
            .FirstOrDefaultAsync(t => t.ThreadsId == threadId);
        if (thread != null)
        {
            thread.Upvotes++;
            await db.SaveChangesAsync();
            return true;
        }
        await db.SaveChangesAsync();
        return true;
    }

}
