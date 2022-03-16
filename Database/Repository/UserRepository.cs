using System.Security.Cryptography;
using System.Text;
using C__tutorials.Interface;
using C__tutorials.Models;
using Microsoft.EntityFrameworkCore;
using C__tutorials.DTO;
using C__tutorials.Models.Responses;

namespace C__tutorials.Repository
{
#pragma warning disable
    public class UserRepository : IUserRepository
    {
        private readonly IService _tokenService;
        private Context _context { get; set; }

        public UserRepository(Context context, IService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        public async Task<OkStatus> Login(UserDto login)
        {
            var user = await _context.user.SingleOrDefaultAsync(x => x.Email == login.Email);
            if (user == null)
            {
                return new OkStatus("User not found", 404, null, false);
            }

            using var hmac = new HMACSHA512(StringToByte(login.Password));
            var computed = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
            for (int i = 0; i < computed.Length; i++)
            {
                if (computed[i] != user.PasswordSalt[i])
                {
                    return new OkStatus("Password is incorrect", 401, null, false);
                }
            }

            var response = new LoginResponse()
            {
                Token = _tokenService.CreateToken(user),
                User = user.Email
            };
            return new OkStatus("Login successful", 200, response, true);
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
                PasswordSalt = ByteToString(hmac.Key),
                Role = register.Role,
                FirstName = register.FirstName,
                LastName = register.LastName,
                Address = "Tashkent,Uzbekistan",
                City = "Tashkent",
                State = "UZB",
                Zip = "100012",
                Phone = "+998997541642",
                CreditCard = "ADMIN=9860250602*****",
                Age = 19
            };
            await _context.user.AddAsync(newUser);
            await _context.SaveChangesAsync();
            var response = new LoginResponse()
            {
                Token = _tokenService.CreateToken(newUser),
                User = newUser.Email
            };
            return new OkStatus("Login successful", 200, response, true);
        }

        private async Task<bool> UserExists(string email)
        {
            return await _context.user.AnyAsync(x => x.Email == email);
        }

        private string ByteToString(byte[] password)
        {
            return Encoding.UTF8.GetString(password);
        }

        private byte[] StringToByte(string password)
        {
            return Encoding.UTF8.GetBytes(password);
        }
    }
}