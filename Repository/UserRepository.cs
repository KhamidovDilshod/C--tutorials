using C__tutorials.Interface;
using C__tutorials.Models;
using Microsoft.EntityFrameworkCore;
using C__tutorials.Utils;
using C__tutorials.DTO;
using C__tutorials.Models.Responses;

namespace C__tutorials.Repository
{
#pragma warning disable
    public class UserRepository : IUserRepository
    {
        public DataContext _context { get; }
        public UserRepository(DataContext context)
        {
            this._context = context;
        }
        public async Task<UserDto> AddUser(UserDto user)
        {
            var encoder = new Util();
            var password = encoder.Encode(user.Password);
            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = password,
                Role = user.Role
            };
            await _context.user.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return user;

        }
        // public async Task<Login> GetAll()
        // {
        //     Login users = new Login(
        //         await _context.user.ToListAsync(),
        //         await _context.user.ToListAsync()
        //     );
        //     return users;
        // }

        public async Task<User> DeleteUser(int id)
        {
            var result = _context.user.FirstOrDefaultAsync(x => x.Id == id).Result;
            if (result != null)
            {
                _context.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            else
            {
                return null;
            }
        }

        public Task<User> GetUser(int id)
        {
            throw new NotImplementedException();
        }
        Task<List<User>> IUserRepository.UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        Task<List<Login>> IUserRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<OkStatus> Login(LoginDto login)
        {
            var encoder = new Util();
            string response = "";
            var password = encoder.Encode(login.Password);
            var result = _context.user.FirstOrDefaultAsync(x => x.Email == login.Email).Result;
            for (int i = 0; i <= password.Length; i++)
            {
                if (password[i] == result.Password[i])
                {
                    response = "Success";
                }
                else
                {
                    response = "Fail";
                }
            }
            return  new OkStatus("Login Successful", 200, response);
        }
    }
}