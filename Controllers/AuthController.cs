using C__tutorials.DTO;
using C__tutorials.Interface;
using C__tutorials.Models;
using Microsoft.AspNetCore.Mvc;

#pragma warning disable
namespace C__tutorials.Controllers
{
    public class AuthController : BaseController
    {
        public IUserRepository _repo { get; }

        public AuthController(IUserRepository repo)
        {
            this._repo = repo;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserDto login)
        {
            var result = await _repo.Login(login);
            Console.WriteLine(result);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(Register register)
        {
            var result = await _repo.Register(register);
            return Ok(result);
        }
    }
}