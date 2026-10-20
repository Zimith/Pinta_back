public class CreateUserResponse
{
    internal string? hashedPassword;

    public long id { get; set; }
    public string username { get; set; } = "";
    public string password { get; set; } = "";
    public string email { get; set; } = "";
    public string avatar { get; set; } = "";
}