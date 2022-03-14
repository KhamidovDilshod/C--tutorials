using C__tutorials.DTO;
using C__tutorials.Interface;
using C__tutorials.Models;
using C__tutorials.Models.Responses;
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
        [HttpGet]
        [Route("users")]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            return Ok(await _repo.GetAll());
        }
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<User>> AddUser(UserDto user)
        {
            var res = await _repo.AddUser(user);
            return Ok(new OkStatus("User Added Successfully", 200, user));
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var res = await _repo.DeleteUser(id);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound(new NotFound($"User with id {id} not found", 404));
            }
            return Ok(res);
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginDto login)
        {
            var res = await _repo.Login(login);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound(new NotFound($"Insufficient Credentials", 404));
            }
        }
    }
}