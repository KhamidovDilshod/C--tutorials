using C__tutorials.Database.DTOs;
using C__tutorials.DTO;
using C__tutorials.Models;
using C__tutorials.Models.Responses;

namespace C__tutorials.Interface
{
    public interface IUserRepository
    {
        Task<OkStatus> Login(UserDto login);
        Task<OkStatus> Register(Register register);
        Task<UserResponse> GetUserByEmail(string email);
        Task<BankResponse> BankLogin(BankUserDTO bankUser);
        Task<OkBro<Accounts>> BankAccounts();
        string BuySellPrices();
        Task<OkBro<ClientDetails>> AllClients();
    }
}