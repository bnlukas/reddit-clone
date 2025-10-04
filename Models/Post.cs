namespace RedditClone.Models;

public class Post
{
    public Post(int id, string title, string content, int upvotes, int downvotes, DateTime created)
    {
        Id = id;
        this.title = title;
        Content = content;
        Upvotes = upvotes;
        Downvotes = downvotes;
        Created = created;
    }

    public int Id { get; set; }
    public string title { get; set; }
    public string Content { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public DateTime Created { get; set; }
}