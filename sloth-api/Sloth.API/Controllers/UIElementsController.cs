using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sloth.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Sloth.Domain.Constants;
using Sloth.Application.Services.UIElements;
using Sloth.Shared.Models;

namespace Sloth.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
// TODO: Authorize controller
//[Authorize]
public class UIElementsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<ActionResult<GetWebPage>> GetLoginWebPage(string appID)
    {
        var result = await mediator.Send(new GetWebPageQuery(appID, WebPages.Auth));
        return Ok(result);
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetWebPage>> GetMainWebPage(string appID)
    {
        var result = await mediator.Send(new GetWebPageQuery(appID, WebPages.Main));
        return Ok(result);
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetWebPage>> GetWebPage([FromQuery] string appID, [FromQuery] string pageID)
    {
        // This will prevent ByPassSecurity from being set to true
        var result = await mediator.Send(new GetWebPageQuery(appID, pageID));
        return Ok(result);
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<GetWebPage>>?> ListWebPageByID([FromQuery] string? appID = null, [FromQuery] string? pageID = null)
    {
        var result = await mediator.Send(new ListWebPageByIDQuery(appID, pageID));
        return Ok(result);
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<string>>?> ListWebApplicationIDs()
    {
        var result = await mediator.Send(new ListWebApplicationIDsQuery());
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WebPageItem?>> GetFullWebPage([FromQuery] string appID, [FromQuery] string pageID)
    {
        var result = await mediator.Send(new GetFullWebPageQuery(appID, pageID));
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
    public async Task<ActionResult<string?>> SaveFullWebPage([FromBody] WebPageItem webPage)
    {
        var result = await mediator.Send(new SaveFullWebPageCommand(webPage));
        return Ok(result);
    }
}
