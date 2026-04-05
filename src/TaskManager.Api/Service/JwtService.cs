using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using TaskManager.Api.Domain;
using TaskManager.Api.Infrastructure;

namespace TaskManager.Api.Service;

public class JwtService
{
    private readonly JwtSettings _settings;

    public JwtService(IOptions<JwtSettings> options)
    {
        _settings = options.Value;
    }

    public string IssueJwt(User user)
    {
        return GenerateJwt(user.Name);
    }

    private string GenerateJwt(string username)
    {
        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);
        string audience = _settings.Audience;
        string issuer = _settings.Issuer;

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, username)
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}