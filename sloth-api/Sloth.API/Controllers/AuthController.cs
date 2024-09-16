using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sloth.Application.Services.Security;

namespace Sloth.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost] 
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }
}
