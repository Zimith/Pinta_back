using System.Security.Cryptography;
using System.Text;
using Pinta.Domain.FileSystem;
using Pinta.Domain.Posts;
using Pinta.Domain.Security;

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

    private ICollection<Comment> comments = new List<Comment>();

    private ICollection<Reaction> reactions = new List<Reaction>();
    private ICollection<Ban> bans = new List<Ban>();

    private ICollection<Post> posts = new List<Post>();

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
    public virtual Image? Avatar
    {
        get { return avatar; }
        set { avatar = value; }
    }
    public virtual Image? Banner
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
    public virtual ICollection<Comment> Comments
    {
        get { return comments; }
        set { comments = value; }
    }
    public virtual ICollection<Reaction> Reactions
    {
        get { return reactions; }
        set { reactions = value; }
    }
    public virtual ICollection<Post> Posts
    {
        get { return posts; }
        set { posts = value; }
    }
    public virtual ICollection<Ban> Bans
    {
        get { return bans; }
        set { bans = value; }
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

    public string? GetAvatar()
    {
        if (this.Avatar != null)
        {
            return "image/" + this.Avatar.Id;
        }
        return null;
    }

    public string? GetBanner()
    {
        if (this.Banner != null)
        {
            return "image/" + this.Banner.Id;
        }
        return null;
    }
}

// ICollection representa una colección de entidades relacionadas (relación 1:N).
// virtual permite que EF Core pueda sobrescribir la propiedad y utilizar Lazy Loading.