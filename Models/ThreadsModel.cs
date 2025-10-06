using System.Diagnostics;

namespace RedditClone.Models;

public class ThreadsModel
{
    public ThreadsModel(int postid, string name ,string title, string content, int upvotes, int downvotes, DateTime created)
    {
        ThreadsId = postid; 
        AuthorName = name;
        Title = title;
        ThreadsContent = content;
        Upvotes = upvotes;
        Downvotes = downvotes;
        Created = created;
    }

    public ThreadsModel()
    {
    } 
    
    //_____ ID
    public string? AuthorName { get; set; }
    public int? ThreadsId { get; set; } 
   
    // ______ INDHOLD 
    public string Title { get; set; } = String.Empty;
    public string? ThreadsContent { get; set; } =String.Empty;// Tekst indhold 
    public List<ThreadsCommentsModel> Comments { get; set; } = new(); 
    
    //________ DATE 
    public DateTime Created { get; set; }
    
    // _______ POPULARITY 
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    
}