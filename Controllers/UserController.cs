using C__tutorials.DTO;
using C__tutorials.Interface;
using C__tutorials.Models;
using Microsoft.AspNetCore.Mvc;

#pragma warning disable
namespace C__tutorials.Controllers
{
    [ApiController]
    [Route("v1")]
    public class UserController : ControllerBase
    {
        public IUserRepository _repo { get; }

        public UserController(IUserRepository repo)
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
            Console.WriteLine(result);
            return Ok(result);
        }
    }
}