using C__tutorials.DTO;
using C__tutorials.Interface;
using C__tutorials.Models;
using Microsoft.AspNetCore.Mvc;

namespace C__tutorials.Controllers;
#pragma warning disable
public class AccountController : BaseController
{
    private readonly IUserRepository _repo;

    public AccountController(IUserRepository repo)
    {
        _repo = repo;
    }
    [HttpGet("accounts/remaining")]
    

    public async Task<ActionResult<Accounts>> GetAccounts()
    {
        var result = _repo.BankAccounts();
        return Ok(result);
    }
    [HttpGet("nbu/currencies")]
    public async Task<ActionResult<List<Currency>>> BuySellPrices()
    {
        var result = _repo.BuySellPrices();
        return Ok(result);
    }

    [HttpGet("users/all")]
    public async Task<ActionResult<List<ClientDetails>>> GetAllClients()
    {
        var res = _repo.AllClients();
        return Ok(res);
    }
}