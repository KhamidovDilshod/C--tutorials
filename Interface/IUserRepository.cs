using C__tutorials.DTO;
using C__tutorials.Models;
using C__tutorials.Models.Responses;

namespace C__tutorials.Interface
{ 
    public interface IUserRepository
    {
        Task<OkStatus>Login(UserDto login);
        Task<OkStatus> Register(Register register);
    }

}