using C__tutorials.DTO;

namespace C__tutorials.Models.Responses
{
    public class OkStatus : BaseResponse
    {
        public Object user { get; set; }
        public OkStatus(string Message, int StatusCode, Object users) : base()
        {
            this.Message = Message;
            this.StatusCode = StatusCode;
            this.Success = true;
            this.user = users;
        }
    }
}