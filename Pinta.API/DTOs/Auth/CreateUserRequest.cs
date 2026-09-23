public class CreateUserRequest
{
    public string username { get; set; } = string.Empty;
    public string hashedPassword { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string Fullname { get; set; } = string.Empty;
    public IFormFile? file { get; set; }
}