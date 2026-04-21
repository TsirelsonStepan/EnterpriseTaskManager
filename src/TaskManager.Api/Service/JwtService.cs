using System;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

using TaskManager.Api.Domain.Model;
using TaskManager.Api.Infrastructure;

namespace TaskManager.Api.Service;

public interface IJwtService
{
    public string GenerateJwt(UserModel userModel);
}

public class JwtService : IJwtService
{
    private readonly JwtSettings _settings;

    public JwtService(IOptions<JwtSettings> options)
    {
        _settings = options.Value;
    }

    public string GenerateJwt(UserModel userModel)
    {
        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);
        string audience = _settings.Audience;
        string issuer = _settings.Issuer;

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, userModel.UserName),
            new Claim(ClaimTypes.NameIdentifier, userModel.UserGuid ?? throw new NullReferenceException("User isn't registered in DB and doesn't have Guid"))
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