using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.Domain.Dto;

public class LoginRequest
{
    [Required]
    public required string Username { get; init; }
    [Required]
    public required string Password { get; init; }
}