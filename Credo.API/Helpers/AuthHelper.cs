using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Credo.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Credo.API.Helpers;

public static class AuthHelper
{
    public static Task<string> GenerateTokenAsync(User user, IConfiguration config)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JwtSettings:Key").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return Task.FromResult(jwt);
    }

    public static Task<string> HashPasswordAsync(string password)
        => Task.FromResult(BCrypt.Net.BCrypt.HashPassword(password));
}
