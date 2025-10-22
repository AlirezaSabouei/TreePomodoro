using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Infrastructure.Tools;

public class TokenService(IConfiguration config) : ITokenService
{
    private readonly IConfiguration _config = config;

    public string GenerateAccessToken(string userId, string role, string email, string name)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(ClaimTypes.Role, role),
            new(ClaimTypes.GivenName, name),
            new(ClaimTypes.Email, email),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: null,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60), // short lifespan
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString("N"); // random opaque string
    }
}
