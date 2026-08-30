namespace Pinta.Domain.Auth;

public class User : Person
{
    #region Private
    private string username = "";
    private string hashedPassword = "";
    private string email = "";
    private Image? avatar;
    private Image? banner;
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
    public Image? Avatar
    {
        get { return avatar; }
        set { avatar = value; }
    }
    public Image? Banner
    {
        get { return banner; }
        set { banner = value; }
    }
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