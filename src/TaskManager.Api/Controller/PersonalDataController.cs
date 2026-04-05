using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using TaskManager.Api.Service;

namespace TaskManager.Api.Controller;

[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class PersonalDataController : ControllerBase
{
    readonly PersonalDataService _personalDataService;

    public PersonalDataController(PersonalDataService personalDataService)
    {
        _personalDataService = personalDataService;
    }

    [Authorize]
    [HttpGet("personal")]
    public async Task<ObjectResult> GetPersonalData()
    {
        string username = (HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException()).Value;
        string data = await _personalDataService.GetUserData(username);
        return Ok(data);
    }
}