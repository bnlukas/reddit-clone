using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model;

public class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    
    
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    
    
    public DateTime Created { get; set; } = DateTime.Now;
    public List<Comment> Comments { get; set; } = new List<Comment>();
    
}
