namespace Pinta.Domain.Profile;
public class Follow
{
    #region Private
    private int id;
    private int followerId;
    private int followingId;
    #endregion

    #region Public
    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public int FollowerId
    {
        get { return followerId; }
        set { followerId = value; }
    }

    public int FollowingId
    {
        get { return followingId; }
        set { followingId = value; }
    }
    #endregion
}