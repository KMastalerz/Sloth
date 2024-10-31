using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sloth.Application.Services.Security;
using Sloth.Domain.DTO;

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

    // TODO: Add Forgot Password

    // TODO: Add Reset Password

    // TODO: Add Change Password

    // TODO: Add Change User Details

    // TODO: Add 2FA
}
