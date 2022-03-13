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
        [HttpGet]
        [Route("users")]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            var res = _repo.GetAll();
            return Ok(res);
        }
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            var res =await _repo.AddUser(user);
            return Ok(res);
        }
    }
}