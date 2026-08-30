namespace Pinta.Domain.Posts;

public class Post
{
    #region Private
    private int id;
    private string description = string.Empty;
    private string? imagePost;
    private string? videoPost;
    private string? gameName;
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

    public string? ImagePost
    {
        get { return imagePost; }
        set { imagePost = value; }
    }

    public string? VideoPost
    {
        get { return videoPost; }
        set { videoPost = value; }
    }

    public string? GameName
    {
        get { return gameName; }
        set { gameName = value; }
    }
    #endregion

    public void EditPost(string description, string? imagePost, string? videoPost, string? gameName)
    {
        Description = description;
        ImagePost = imagePost;
        VideoPost = videoPost;
        GameName = gameName;
    }

    public void DeletePost()
    {
    }
}
