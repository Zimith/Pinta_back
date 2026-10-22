public class UserProfileResponse
{
    public string? username {get; set; }
    public string? avatar {get; set; }
    public string? banner {get; set; }
    public int followsCount {get; set; }
    public int? followingCount {get; set; }
    public int? postsCount {get; set; }
    
    /*
    public string? description {get; set; }
    public string? country {get; set; }
    public DataTime? joinData {get; set; }
    Falta agregar estos datos en USER, despeus lo hago
    */
}