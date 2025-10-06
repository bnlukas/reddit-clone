using System.ComponentModel.DataAnnotations;

namespace RedditClone.Models;

public class ThreadsCommentsModel
{

    public ThreadsCommentsModel() {}
    
    //______ ID 
    public int AuthorId { get; set; }
    
    [Required] [MaxLength(20)]
    public string AuthorName { get; set; }

    public int PostId { get; set; }
    
    [Key]
    public int CommentId { get; set; }
    //_____ INDHOLD 

    [MaxLength(1000)]
    public string Comments { get; set; } 
    
    // _____ DATES 
    public DateTime Created { get; set; } =  DateTime.UtcNow;
    
    
    //____ POPULARITY 
    public int UpVotes { get; set; }
    
    public int DownVotes { get; set; }
}