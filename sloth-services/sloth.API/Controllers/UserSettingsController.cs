using MediatR;
using Microsoft.AspNetCore.Mvc;
using sloth.Application.Services.UserSettings;

namespace sloth.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserSettingsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UpdateInfo(UpdateInfoCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> UpdatePassword(UpdatePasswordCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> AddUserRole(AddUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> RemoveUserRole(RemoveUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}
