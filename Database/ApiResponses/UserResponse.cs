using C__tutorials.Models;
using C__tutorials.Models.Responses;

namespace C__tutorials.DTO;

public class UserResponse:BaseResponse
{
    #pragma warning disable
 
    public User User { get; set; }

    public UserResponse(int statusCode,string message, User? user, bool success)
    {
        this.StatusCode = statusCode;
        this.User = user;
        this.Message = message;
        this.Success = success;
    }
}