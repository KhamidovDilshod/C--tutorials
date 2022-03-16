using System.Security.Cryptography;
using System.Text;
using C__tutorials.Interface;
using C__tutorials.Models;
using Microsoft.EntityFrameworkCore;
using C__tutorials.DTO;
using C__tutorials.Models.Responses;
using C__tutorials.Utils;

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
            Console.WriteLine(user);
            if (user == null)
            {
                return new OkStatus("User not found", 404, null, false);
            }

            using var hmac = new HMACSHA512(Util.StringToByte(login.Password));
            Console.WriteLine(login.Password);
            var computed = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.PasswordSalt));
            for (int i = 0; i < computed.Length; i++)
            {
                Console.WriteLine(computed[i]);
                Console.WriteLine(computed);
                if (computed[i] != Util.StringToByte(user.PasswordSalt)[i])
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

            var newUser = new User()
            {
                Email = register.Email,
                Password = Util.ByteToString(Util.Encode(register.Password)),
                PasswordSalt = Util.ByteToString(Util.PasswordSalt(register.Password)),
                Role = register.Role,
                FirstName = register.FirstName,
                LastName = register.LastName,
                Address = "Tashkent,Uzbekistan",
                City = "Tashkent",
                State = "",
                Zip = "100012",
                Phone = "",
                CreditCard = "ADMIN=98602*********",
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

        public async Task<UserResponse> GetUserByEmail(string email)
        {
            if (await UserExists(email))
            {
                var result = await _context.user.Where(x => x.Email == email).ToListAsync();
                User user= new User()
                {
                    Email = result[0].Email,
                    Password = "Your password is hidden",
                    Role = result[0].Role,
                    FirstName = result[0].FirstName,
                    LastName = result[0].LastName,
                    Address = result[0].Address,
                    City = result[0].City,
                    State = result[0].State,
                    Zip = result[0].Zip,
                    Phone = result[0].Phone,
                    CreditCard = result[0].CreditCard,
                    Age = result[0].Age
                };
                return new UserResponse(200,"Content", user, true);
            }

            return new UserResponse(404, "User not found",null, false);
        }

        private async Task<bool> UserExists(string email)
        {
            return await _context.user.AnyAsync(x => x.Email == email);
        }
    }
}