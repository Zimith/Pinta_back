namespace Pinta.Domain.Auth;
//Presiona Ctrl + K seguido de Ctrl + C para comentar
//Presiona Ctrl + K seguido de Ctrl + U para descomentar
public class User : Person
{
    #region Private
    private string username = "";
    private string hashedPassword = "";
    private string email = "";
    // private Image? avatar=null;
    // private Image? banner=null;
    private DateTime registrationDate;
    private bool isBanned= false;
    private RoleType roleType = RoleType.User;
    #endregion

    #region Public
    public string Username
    {
        get { return username; }
        set { username = value; }
    }
    public string HashedPassword
    {
        get { return hashedPassword; }
        set { hashedPassword = value; }
    }
    public string Email
    {
        get { return email; }
        set { email = value; }
    }
    // public virtual Image? Avatar
    // {
    //     get { return avatar; }
    //     set { avatar = value; }
    // }
    // public virtual Image? Banner
    // {
    //     get { return banner; }
    //     set { banner = value; }
    // }
    public DateTime RegistrationDate
    {
        get { return registrationDate; }
        set { registrationDate = value; }
    }
    public bool IsBanned
    {
        get { return isBanned; }
        set { isBanned = value; }
    }
    public RoleType Role
    {
        get { return roleType; }
        set { roleType = value; }
    }
    #endregion
}