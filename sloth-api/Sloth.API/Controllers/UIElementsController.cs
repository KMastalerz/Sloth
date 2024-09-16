using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sloth.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UIElementsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public ActionResult<string> TestAuth()
    {
        return Ok("Authorized");
    }
}
