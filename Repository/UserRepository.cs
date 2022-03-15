using System.Security.Cryptography;
using System.Text;
using C__tutorials.Interface;
using C__tutorials.Models;
using Microsoft.EntityFrameworkCore;
using C__tutorials.DTO;
using C__tutorials.Models.Responses;
using Tutorials;

namespace C__tutorials.Repository
{
#pragma warning disable
    public class UserRepository : IUserRepository
    {
        private readonly IService _tokenService;
        public Context _context { get; set; }

        public UserRepository(Context context, IService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }


        public async Task<OkStatus> Login(UserDto login)
        {
            LoginResponse loginResponse = new LoginResponse();
            if (await UserExists(login.Email))
            {
                return new OkStatus("User already exists", 404,loginResponse,false);
            }

            using var hmac = new HMACSHA512();
            var user = new User
            {
                Email = login.Email,
                Password = ByteToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password))),
                Role = login.Role
            };
            var res = new LoginResponse()
            {
                Token = _tokenService.CreateToken(user),
                User = user.Email
            };
            _context.user.Add(user);
            await _context.SaveChangesAsync();
            return new OkStatus("User created", 200, res,true);
        }

        public async Task<OkStatus> Register(Register register)
        {
            LoginResponse loginResponse = new LoginResponse();
            if (await UserExists(register.Email))
            {
                return new OkStatus("User already exists", 404, loginResponse, false);
            }

            using var hmac = new HMACSHA512();
            var newUser = new User()
            {
                Email = register.Email,
                Password = ByteToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password))),
                Role = register.Role,
                FirstName = register.FirstName,
                LastName = register.LastName,
            };
            await _context.user.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return new OkStatus("User created", 200, loginResponse, true);
        }

        private async Task<bool> UserExists(string email)
        {
            return await _context.user.AnyAsync(x => x.Email == email);
        }

        private string ByteToString(byte[] password)
        {
            return Encoding.UTF8.GetString(password);
        }
    }
}