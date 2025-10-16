using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model;

public class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;

    public List<Comment> Comments { get; set; } = new List<Comment>();
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public Post() {}

    public Post(User user, string title, string content, int upvotes = 0, int downvotes = 0)
    {
        User = user; 
        Title = title;
        Content = content;
        Upvotes = upvotes;
        Downvotes = downvotes;
    }

}
