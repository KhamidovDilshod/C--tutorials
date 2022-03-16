using C__tutorials.Interface;
using C__tutorials.Models;
using C__tutorials.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace C__tutorials.Controllers;

public class UserController : BaseController
{
    private readonly IUserRepository _repo;

    public UserController(IUserRepository repo)
    {
        _repo = repo;
    }

    [Authorize]
    [HttpGet]
    [Route("user/{Email}")]
    public async Task<ActionResult<User>> GetUserDetails(string Email)
    {
        var result = await _repo.GetUserByEmail(Email);
        return Ok(result);
    }
}