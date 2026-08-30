using System.Security.Cryptography;
using System.Text;

namespace Pinta.Domain.Auth;

public class User : Person
{
    #region Private
    private string username = "";
    private string hashedPassword = "";
    private string email = "";
    // private Image? avatar;
    // private Image? banner;
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
    // public Image? Avatar
    // {
    //     get { return avatar; }
    //     set { avatar = value; }
    // }
    // public Image? Banner
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

    private static string encript(string hashedPassword)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(hashedPassword);
        byte[] bytesHash = SHA1.HashData(bytes);

        return Convert.ToBase64String(bytesHash);
    }

    public virtual void SetPassword(string hashedPassword)
    {
        this.HashedPassword = User.encript(hashedPassword);
    }

    public virtual bool IsPassword(string hashedPassword)
    {
        string passEncripted = User.encript(hashedPassword);
        if (this.HashedPassword == passEncripted)
        {
            return true;
        }

        return false;
    }
}