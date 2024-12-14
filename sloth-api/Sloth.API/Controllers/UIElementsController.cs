using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sloth.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Sloth.Domain.Constants;
using Sloth.Application.Services.UIElements;

namespace Sloth.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
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
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<GetWebPage>>?> ListWebPageByID([FromQuery] string? appID = null, [FromQuery] string? pageID = null)
    {
        var result = await mediator.Send(new ListWebPageByIDQuery(appID, pageID));
        return Ok(result);
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<string>>?> ListWebApplicationIDs()
    {
        var result = await mediator.Send(new ListWebApplicationIDsQuery());
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetWebPageFull?>> GetFullWebPage([FromQuery] string appID, [FromQuery] string pageID)
    {
        var result = await mediator.Send(new GetFullWebPageQuery(appID, pageID));
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetWebPageFull>> SaveFullWebPage([FromBody] GetWebPageFull webPage)
    {
        var result = await mediator.Send(new SaveFullWebPageCommand(webPage));
        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteWebPage([FromQuery] string appID, [FromQuery] string pageID)
    {
        await mediator.Send(new DeleteWebPageCommand(appID, pageID));
        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ListWebPagesItem>> ListWebPages()
    {
        var result = await mediator.Send(new ListWebPagesQuery());
        return Ok(result);
    }
}
