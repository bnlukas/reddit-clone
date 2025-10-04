using RedditClone.Models;
namespace RedditClone.Service;

public class DataService
{
    private RedditContent db { get;  }

    public DataService(RedditContent db)
    {
        this.db = db;
    }

    public void SeedData()
    {
        ThreadsModel threads = db.Authors.FirstOrDefault()!;
        if (threads == null)
        {
            threads = new ThreadsModel
            {
                AuthorId = 1,
                PostId = 1, 
                AuthorName = "NoobMaster47", 
                Title = "Hvordan bager man drømmekage?",
                Content = "",
                Created = DateTime.UtcNow,
            }; 
        }
    }
}