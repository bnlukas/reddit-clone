namespace RedditClone.Models;

public class ThreadsCommentsModel
{

    public ThreadsCommentsModel() {}
    //______ ID 
    public int AuthorId { get; set; }
    
    public string AuthorName { get; set; }

    public int PostId { get; set; }
    public int CommentId { get; set; }
    //_____ INDHOLD 

    public List<Comment> Comments { get; set; } = new List<Comment>(); 
    
    // _____ DATES 
    public DateTime Created { get; set; } =  DateTime.UtcNow;
    
    
    //____ POPULARITY 
    public int UpVotes { get; set; }
    
    public int DownVotes { get; set; }
}