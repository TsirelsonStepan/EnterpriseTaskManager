using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using TaskManager.Api.Domain.Dto;
using TaskManager.Api.Service;

namespace TaskManager.Api.Controller;

[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest userData)
    {
        string jwt = await _authService.Login(userData.Username, userData.Password);
        return Ok(new JwtResponse(jwt));
    }

    [HttpPost("register")]
    public async Task<StatusCodeResult> Register([FromBody] RegisterRequest userData)
    {
        await _authService.Register(userData.Username, userData.Password);
        return Ok();
    }   
}