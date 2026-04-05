using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.DTO.Request;

public class LoginRequest
{
    [Required]
    public required string Username { get; init; }
    [Required]
    public required string Password { get; init; }
}