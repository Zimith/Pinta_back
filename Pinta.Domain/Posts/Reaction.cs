using Pinta.Domain.Auth;

namespace Pinta.Domain.Posts;

public class Reaction
{
    #region Private
    private int id;
    private Post post = null!;
    private User user = null!;
    private int postId;
    private int userId;
    private ReactionType reactionType;
    #endregion

    #region Public
    public int PostId
    {
        get { return postId; }
        set { postId = value; }
    }

    public int UserId
    {
        get { return userId; }
        set { userId = value; }
    }

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public ReactionType ReactionType
    {
        get { return reactionType; }
        set { reactionType = value; }
    }

    public virtual Post Post
    {
        get { return post; }
        set { post = value; }
    }

    public virtual User User
    {
        get { return user; }
        set { user = value; }
    }
    #endregion
}