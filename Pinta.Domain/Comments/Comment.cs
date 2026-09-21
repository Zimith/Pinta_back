using Pinta.Domain.Auth;
using Pinta.Domain.Posts;

namespace Pinta.Domain.Comments;

public class Comment
{
    #region Private
    private int id;
    private string description = string.Empty;
    private int userId;
    private int postId;
    private User user = null!;
    private Post post = null!;
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

    public int UserId
    {
        get { return userId; }
        set { userId = value; }
    }

    public int PostId
    {
        get { return postId; }
        set { postId = value; }
    }

    public User User
    {
        get { return user; }
        set { user = value; }
    }

    public Post Post
    {
        get { return post; }
        set { post = value; }
    }
    #endregion

    public void Edit()
    {
    }

    public void Delete()
    {
    }
}