using C__tutorials.Database.DTOs;
using C__tutorials.Interface;
using C__tutorials.Models;
using Microsoft.AspNetCore.Mvc;

namespace C__tutorials.Controllers;
#pragma warning disable
public class UserController : BaseController
{
    private readonly IUserRepository _repo;

    public UserController(IUserRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    [Route("user/{Email}")]
    public async Task<ActionResult<User>> GetUserDetails(string Email)
    {
        var result = await _repo.GetUserByEmail(Email);
        return Ok(result);
    }

    [HttpPost]
    [Route("bank/login")]
    public async Task<ActionResult<BankUser>> Login(BankUserDTO user)
    {
        var result = await _repo.BankLogin(user);
        return Ok(result);
    }
}