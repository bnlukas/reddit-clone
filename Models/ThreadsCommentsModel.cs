using System.ComponentModel.DataAnnotations;

namespace RedditClone.Models;

public class ThreadsCommentsModel
{

    public ThreadsCommentsModel() {}
    
    //______ ID 
    public int AuthorId { get; set; }
    
     [MaxLength(20)]
    public required string AuthorName { get; set; }

    public int ThreadsId { get; set; }
    
    [Key]
    public int CommentId { get; set; }
    //_____ INDHOLD 

    [MaxLength(1000)]
    public required string Comments { get; set; } 
    
    // _____ DATES 
    public DateTime Created { get; set; } =  DateTime.UtcNow;
    
    
    //____ POPULARITY 
    public int UpVotes { get; set; }
    
    public int DownVotes { get; set; }
}