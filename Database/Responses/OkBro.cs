namespace C__tutorials.Models.Responses;
#pragma warning disable
public class OkBro<T> where T : class
{
    public string Message { get; set; }
    public List<T> Data { get; set; }
    public bool Success { get; set; }
    public int StatusCode { get; set; }

    public OkBro(string message, List<T> data, bool success, int statusCode)
    {
        Message = message;
        Data = data;
        Success = success;
        StatusCode = statusCode;
    }
}