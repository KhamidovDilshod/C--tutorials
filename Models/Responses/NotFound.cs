namespace C__tutorials.Models.Responses
{
    public class NotFound:BaseResponse
    {
        public NotFound(string Message,int StatusCode):base()
        {
            this.Message = Message;
            this.StatusCode = StatusCode;
            this.Success = false;
        }

    }
}