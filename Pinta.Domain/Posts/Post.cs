namespace Pinta.Domain.Posts;

public class Post
{
    #region Private
    private int id;
    private string description = string.Empty;
    private DateTime creationDate;
    private string? image;
    private string? game;
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
    #endregion

    public void Edit()
    {
    }

    public void Delete()
    {
    }
}
