using System.Net;
using System.Security.Cryptography;
using System.Text;
using C__tutorials.Database.DTOs;
using C__tutorials.Interface;
using C__tutorials.Models;
using Microsoft.EntityFrameworkCore;
using C__tutorials.DTO;
using C__tutorials.Models.Responses;
using C__tutorials.Utils;
using Newtonsoft.Json;

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
                Token = _tokenService.CreateToken(login.Email),
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
                Token = _tokenService.CreateToken(register.Email),
                User = newUser.Email
            };
            return new OkStatus("Login successful", 200, response, true);
        }

        public async Task<UserResponse> GetUserByEmail(string email)
        {
            if (await UserExists(email))
            {
                var result = await _context.user.Where(x => x.Email == email).ToListAsync();
                User user = new User()
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
                return new UserResponse(200, "Content", user, true);
            }

            return new UserResponse(404, "User not found", null, false);
        }

        public async Task<BankResponse> BankLogin(BankUserDTO bankUser)
        {
            var result = await _context.BankUsers.SingleOrDefaultAsync(x => x.Username == bankUser.Username);
            Console.WriteLine(result);
            if (result != null)
            {
                if (result.Password == bankUser.Password)
                {
                    var response = new BankResponse()
                    {
                        Token = _tokenService.CreateToken(bankUser.Username),
                        UserName = result.Username,
                        IsSuccess = true,
                        Message = "Login successful",
                        Role = result.Role,
                        statusCode = 200
                    };
                    return response;
                }
            }

            return new BankResponse("Login failed", false, "", "null", "null", 401);
        }

        public async Task<OkBro<Accounts>> BankAccounts()
        {
            var result = _context.Accounts.ToList();
            Console.WriteLine(result);
            return new OkBro<Accounts>("Accounts", result, true, 200);
        }

        public string BuySellPrices()
        {
            var today = Util.DateConvert(DateTime.Now);
            WebClient client = new WebClient();
            var json2 = client.DownloadString("https://nbu.uz/exchange-rates/json");
            var model = JsonConvert.DeserializeObject<List<Currency>>(json2);
            var lastSet = _context.Currencies.Max(entry => entry.date);
            Console.WriteLine(lastSet);
            Console.WriteLine(model);

            for (var i = 0; i < model.Count; i++)
            {
                var currency = new Currency()
                {
                    date = model[i].date,
                    title = model[i].title,
                    cb_price = model[i].cb_price,
                    nbu_buy_price = model[i].nbu_buy_price,
                    nbu_sell_price = model[i].nbu_sell_price,
                    code = model[i].code
                };
                var nbu_date = model[i].date.Substring(0, 10);
            }

            return json2;
        }

        public async Task<OkBro<ClientDetails>> AllClients()
        {
            var result = _context.ClientDetailsEnumerable.ToList();
            return new OkBro<ClientDetails>("Success", result, true, 200);
        }

        private async Task<bool> UserExists(string email)
        {
            return await _context.user.AnyAsync(x => x.Email == email);
        }

        private void AddData()
        {
            var user = new Accounts()
            {
                Branch = "00981",
                Ledger = 2020600695,
                Remaining = 4123843,
                Account = "QUDRATOV SHERALI IBROXIM O'G'LI"
            };
            _context.Accounts.Add(user);
            _context.SaveChanges();
        }
    }
}