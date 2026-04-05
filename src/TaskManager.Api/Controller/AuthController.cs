using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.DTO;
using TaskManager.Api.DTO.Request;
using TaskManager.Api.Service;

namespace TaskManager.Api.Controller;

[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class AuthController : ControllerBase
{
    readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ObjectResult> Login([FromBody] LoginRequest userData)
    {
        string jwt = await _authService.LoginUser(userData.Username, userData.Password);
        return Ok(new JwtResponse(jwt));
    }

    [HttpPost("register")]
    public async Task<StatusCodeResult> Register([FromBody] RegisterRequest userData)
    {
        await _authService.RegisterUser(userData.Username, userData.Password);
        return Ok();
    }

    [HttpPost("remove")]
    public async Task<StatusCodeResult> UnRegister([FromBody] RegisterRequest userData)
    {
        await _authService.RemoveUserByName(userData.Username);
        return Ok();
    }
}