using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model;

public class Comment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; } 
    public Post? Post { get; set; } 
    public int PostId { get; set; }
    
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public string Content { get; set; } = ""; 
    public DateTime Created { get; set; } = DateTime.Now;
}