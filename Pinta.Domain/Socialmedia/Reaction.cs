using Pinta.Domain.Auth;
using Pinta.Domain.Socialmedia;

namespace Pinta.Domain.Socialmedia;

public class Reaction
{
    private int id;
    // private Post post;
    private User user = new User();
    // private int postId;
    private int userId;

    private ReactionType reactionType;

    // public int PostId { get; set; }
    public int UserId { get; set; }

    public int Id
    {
        get { return this.id; }
        set { this.id = value; }
    }

    public virtual ReactionType ReactionType
    {
        get { return this.reactionType; }
        set { this.reactionType = value; }
    }

    // public virtual Post Post
    // {
    //     get { return this.post; }
    //     set { this.post = value; }
    // }

    public virtual User User
    {
        get { return this.user; }
        set { this.user = value; }
    }
}