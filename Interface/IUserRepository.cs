using C__tutorials.DTO;
using C__tutorials.Models;
using C__tutorials.Models.Responses;

namespace C__tutorials.Interface
{ 
    public interface IUserRepository
    {
        Task<List<Login>> GetAll();
        Task<User> GetUser(int id);
        Task<UserDto> AddUser(UserDto user);
        Task<List<User>> UpdateUser(User user);
        Task<User> DeleteUser(int id);
        Task<OkStatus>Login(LoginDto login);
    }

}