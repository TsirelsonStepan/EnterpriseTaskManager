using TaskManager.Api.Domain;
using TaskManager.Api.Repository;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;

namespace TaskManager.Api.Service;

public class AuthService
{
    readonly UserRepository _userRepository;
    readonly JwtService _jwtService;
    readonly PasswordHasher<User> _hasher = new();

    public AuthService(UserRepository userRepository, JwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    //cRud
    public async Task<string> LoginUser(string username, string password)
    {
        UserEntity userEntity = await _userRepository.SearchByUsername(username);
        User user = new(userEntity.Name!/*NOT NICE*/);
        PasswordVerificationResult verificationResult = _hasher.VerifyHashedPassword(user, userEntity.PasswordHash!/*NOT NICE*/, password);
        if (verificationResult == PasswordVerificationResult.Failed) throw new UnauthorizedAccessException("password verficatioin failed");
        string jwt = _jwtService.IssueJwt(user);
        return jwt;
    }

    //Crud
    public async Task RegisterUser(string username, string password)
    {
        User newUser = new(username);
        string hash = _hasher.HashPassword(newUser, password);
        await _userRepository.CreateUser(newUser, hash);
    }

    //cruD
    public async Task RemoveUserByName(string username)
    {
        UserEntity userToRemove = await _userRepository.SearchByUsername(username);
        await _userRepository.DeleteUser(userToRemove);
    }
}