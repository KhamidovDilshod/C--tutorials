using C__tutorials.DTO;

namespace C__tutorials.Models.Responses
{
    public class OkStatus : BaseResponse
    {
        public Object user { get; set; }

        public OkStatus(string Message, int StatusCode, LoginResponse? loginRes, bool success) : base()
        {
            this.Message = Message;
            this.StatusCode = StatusCode;
            this.user = loginRes;
            this.Success = success;
        }
    }
}