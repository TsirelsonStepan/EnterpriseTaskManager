using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using TaskManager.Api.Domain.Model;
using TaskManager.Api.Domain.Entity;
using TaskManager.Api.Infrastructure;
using TaskManager.Api.Exception;

namespace TaskManager.Api.Service;

public interface IAuthService
{
    public Task<string> Login(string username, string password);
    public Task Register(string username, string password);
}

public class AuthService : IAuthService
{
    readonly TaskManagerDbContext _context;
    readonly JwtService _jwtService;
    readonly PasswordHasher<UserModel> _hasher = new();

    public AuthService(TaskManagerDbContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<string> Login(string username, string password)
    {
        UserEntity userEntity = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username) ?? throw new EntityNotFoundException($"User entity corresponding to username '{username}' was not found");
        if (userEntity.PasswordHash == null) throw new System.Exception($"Unexpected event: PasswordHash is missing for {username}.");
        UserModel userModel = new(
            guid: userEntity.UserGuid,
            name: userEntity.UserName,
            data: userEntity.UserData
        );
        PasswordVerificationResult verificationResult = _hasher.VerifyHashedPassword(userModel, userEntity.PasswordHash, password);
        if (verificationResult == PasswordVerificationResult.Failed) throw new UnauthorizedAccessException("Password verficatioin failed");
        string jwt = _jwtService.GenerateJwt(userModel);
        return jwt;
    }

    public async Task Register(string username, string password)
    {
        UserModel userModel = new(username);
        string passwordHash = _hasher.HashPassword(userModel, password);
        UserEntity userEntity = new(
            name: userModel.UserName,
            passwordHash: passwordHash
        );
        _context.Users.Add(userEntity);
        await _context.SaveChangesAsync();
    }
}