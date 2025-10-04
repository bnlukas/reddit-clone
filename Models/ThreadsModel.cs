namespace RedditClone.Models;

public class ThreadsModel
{
    public ThreadsModel(int id, string name , string title, string content, int upvotes, int downvotes, DateTime created)
    {
        AuthorId = id; 
        AuthorName = name;
        Title = title;
        Content = content;
        Upvotes = upvotes;
        Downvotes = downvotes;
        Created = created;
    }

    public ThreadsModel()
    {
    } 
    
    //_____ ID
    public int AuthorId { get; set; }
    public string? AuthorName { get; set; }
    public int PostId { get; set; } 
    // ______ INDHOLD 
    public string Title { get; set; }
    public string Content { get; set; }
    
    //________ DATE 
    public DateTime Created { get; set; }
    
    // _______ POPULARITY 
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    
}