namespace RedditClone.Models;

public class ThreadsModel
{
    public ThreadsModel(int id, string title, string content, int upvotes, int downvotes, DateTime created)
    {
        AuthorId = id;
        Title = title;
        Content = content;
        Upvotes = upvotes;
        Downvotes = downvotes;
        Created = created;
    }

    public ThreadsModel()
    {
    } 

    public int AuthorId { get; set; }
    
    public string Title { get; set; }
    public string Content { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public DateTime Created { get; set; }
}