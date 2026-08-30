namespace Pinta.Domain.Comments;

public class Comment
{
    #region Private
    private int id;
    private string description = string.Empty;
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
    #endregion

    public void EditComment(string description)
    {
        Description = description;
    }

    public void DeleteComment()
    {
    }
}