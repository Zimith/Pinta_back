namespace Pinta.Domain.Profile;
public class Portadaperfil
{
    #region Private
    private int id;
    private int userId;
    private string imageUrl = "";
    #endregion

    #region Public
    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public int UserId
    {
        get { return userId; }
        set { userId = value; }
    }

    public string ImageUrl
    {
        get { return imageUrl; }
        set { imageUrl = value; }
    }
    #endregion
}