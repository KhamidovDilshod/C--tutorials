namespace C__tutorials.Models.Responses
{
    #pragma warning disable
    public class BaseResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public bool Success { get; set; }
    }
}