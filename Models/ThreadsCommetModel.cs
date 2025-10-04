namespace RedditClone.Models;

public class ThreadsCommentModel
{
    public ThreadsCommentModel(string content)
    {
        Content = content;
    }
    public ThreadsCommentModel() {}
    
    public int AuthorId { get; set; }
    
    public string AuthorName { get; set; }
    
    public string Comments { get; set; }
    public string Content { get; set; }
    
    public int PostId { get; set; }
    
    public DateTime Created { get; set; } =  DateTime.UtcNow;
    
    public int Upvotes { get; set; }
    
    public int Downvotes { get; set; }
}