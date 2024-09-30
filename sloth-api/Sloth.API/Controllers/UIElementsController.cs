using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sloth.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Sloth.Domain.Constants;
using Sloth.Application.Services.UIElements;

namespace Sloth.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
public class UIElementsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<ActionResult<GetWebPage>> GetLoginWebPage()
    {
        var result = await mediator.Send(new GetWebPageQuery(WebPages.Auth));
        return Ok(result);
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetWebPage>> GetMainWebPage()
    {
        var result = await mediator.Send(new GetWebPageQuery(WebPages.Main));
        return Ok(result);
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetWebPage>> GetWebPage([FromQuery] string pageID)
    {
        // This will prevent ByPassSecurity from being set to true
        var result = await mediator.Send(new GetWebPageQuery(pageID));
        return Ok(result);
    }
}
