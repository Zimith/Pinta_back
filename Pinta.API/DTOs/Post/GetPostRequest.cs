public class GetPostRequest
{
    public int currentPage { get; set; } = 1;
    public int pageSize { get; set; } = 12;
    public string orderBy { get; set; } = "CreatedAt";
    public string orderDirection { get; set; } = "desc";
    public string search { get; set; } = String.Empty;
}