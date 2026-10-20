public class ResponseDTO <T>
{
    public bool success { get; set; }
    public string? message { get; set; }
    public int code { get; set; }
    public T? payload { get; set; }
}

