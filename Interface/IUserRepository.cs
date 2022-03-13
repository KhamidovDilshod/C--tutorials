using C__tutorials.Models;

namespace C__tutorials.Interface
{
    public interface IUserRepository
    {
        Task<List< User>> GetAll();
        Task<User> GetUser(int id);
        Task<List<User>> AddUser(User user);
        Task<List<User>> UpdateUser(User user);
        Task<User> DeleteUser(int id);
    }

}