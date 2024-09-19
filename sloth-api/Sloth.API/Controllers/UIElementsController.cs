using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sloth.Application.Services.UIElements;
using Sloth.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Sloth.Domain.Constants;

namespace Sloth.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UIElementsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<GetWebPageDTO?>> GetLoginWebPage()
    {
        var result = await mediator.Send(new GetWebPageQuery(WebPages.Login));
        return Ok(result);
    }
    [HttpGet]
    public async Task<ActionResult<GetWebPageDTO?>> GetMainWebPage()
    {
        var result = await mediator.Send(new GetWebPageQuery(WebPages.MainPage));
        return Ok(result);
    }
    [HttpGet]
    public async Task<ActionResult<GetWebPageDTO?>> GetWebPage(GetWebPageQuery query)
    {
        var result = await mediator.Send(query);
        return Ok(result);
    }
}
