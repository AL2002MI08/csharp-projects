using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GameCatalogApi.Services;

public class TokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config) => _config = config;

    public string GenerateToken(string email, string role)
    {
        var jwt = _config.GetSection("Jwt");
        var keyString = jwt["Key"]!;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString))
        {
            KeyId = keyString
        };
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Name, email),
            new Claim(ClaimTypes.Role, role),
            new Claim("role", role)
        };

        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(jwt["ExpiresInMinutes"]!)),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
