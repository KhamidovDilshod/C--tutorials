using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using C__tutorials.Interface;
using C__tutorials.Models;
using Microsoft.IdentityModel.Tokens;

namespace C__tutorials.Services;

public class TokenService : IService
{
    private readonly SymmetricSecurityKey _key;

    public TokenService(IConfiguration config)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Key"]));
    }

    public string CreateToken(string UserName)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, UserName)
        };
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddHours(2),
            SigningCredentials = creds
        };
        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}