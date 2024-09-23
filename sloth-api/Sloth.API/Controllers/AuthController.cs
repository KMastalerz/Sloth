using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sloth.Application.DTO.Security;
using Sloth.Application.Services.Security;

namespace Sloth.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        await mediator.Send(command);
        return Created();
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<AccessTokenResponse>> Login(LoginCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<AccessTokenResponse>> RefreshToken(RefreshTokenCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    //TO DO: Add Forgot Password

    //TO DO: Add Reset Password

    //TO DO: Add Change Password

    //TO DO: Add Change User Details

    //TO DO: Add 2FA
}
