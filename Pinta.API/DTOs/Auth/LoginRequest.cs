public class LoginRequest
{
    public long id{get; set;}
    
    public string username { get; set; } = "";
    public string hashedPassword { get; set; } = "";
}