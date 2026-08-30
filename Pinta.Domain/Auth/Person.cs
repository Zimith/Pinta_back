namespace Pinta.Domain.Auth;
public class Person
{
    #region Private
    private int id;
    private string fullname= "";
    #endregion

    #region Public
    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Fullname
    {
        get { return fullname; }
        set { fullname = value; }
    }
    #endregion
}