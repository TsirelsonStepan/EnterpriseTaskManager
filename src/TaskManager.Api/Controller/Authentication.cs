using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.DTO.Request;


namespace TaskManager.Api.Controller;

[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class AuthController : ControllerBase
{
	[HttpPost("register")]
	public async Task<StatusCodeResult> RegisterUser([FromBody] UserToRegister newUser)
	{
		
		return Ok();
	}
}