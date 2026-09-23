using Pinta.Domain.Auth;

namespace Pinta.Domain.Posts;

public class Post
{
    #region Private
    private int id;
    private string description = string.Empty;
    private DateTime creationDate;
    private string? image;
    private string? game;
    private User user = null!;
    private int userId;
    private ICollection<Comment> comments = new List<Comment>();
    private ICollection<Reaction> reactions = new List<Reaction>();
    #endregion

    #region Public
    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public DateTime CreationDate
    {
        get { return creationDate; }
        set { creationDate = value; }
    }

    public string? Image
    {
        get { return image; }
        set { image = value; }
    }

    public string? Game
    {
        get { return game; }
        set { game = value; }
    }
    public int UserId
    {
        get { return userId; }
        set { userId = value; }
    }
    public virtual User User
    {
        get { return user; }
        set { user = value; }
    }
    public virtual ICollection<Comment> Comments
    {
        get { return comments; }
        set { comments = value; }
    }
    public virtual ICollection<Reaction> Reactions
    {
        get { return reactions; }
        set { reactions = value; }
    }
    #endregion

    public void Edit()
    {
    }

    public void Delete()
    {
    }
}
