namespace C__tutorials.Models.Responses
{
#pragma warning disable

    public class NotFound : BaseResponse
    {
        public NotFound(string Message, int StatusCode) : base()
        {
            this.Message = Message;
            this.StatusCode = StatusCode;
        }
    }
}