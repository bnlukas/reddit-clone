using Microsoft.EntityFrameworkCore;
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
                PostId = 1,  
                AuthorName = "NoobMaster47", 
                Title = "Hvordan bager man drømmekage?",
                ThreadsContent = "",
                Comments = new List<ThreadsCommentsModel>(), 
                Created = DateTime.UtcNow,
            }; 
        }
    }

    GetAllPost();
    GetPost(int postID); 
    CreatePost(ThreadsModel post);
    AddComment(int postId, ThreadsCommentsModel comment);
    UpvotePost(int postID);
    DownvotePost(int postId);
    

}