namespace RedditClone.Models;

public class ThreadsCommetModel
{
    public ThreadsCommetModel(string content)
    {
        Content = content;
    }
    public ThreadsCommetModel() {}

    public int AuthorId { get; set; }
    public string Content { get; set; }
    
    public DateTime Created { get; set; } =  DateTime.UtcNow;
}