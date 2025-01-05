using MediatR;
using Microsoft.AspNetCore.Mvc;
using sloth.Application.Services.Auth;

namespace sloth.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        await mediator.Send(command);
        return Created();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
    //[HttpPost]
    //public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
    //{
    //    var result = await mediator.Send(command);
    //    return Ok(result);
    //}
}
