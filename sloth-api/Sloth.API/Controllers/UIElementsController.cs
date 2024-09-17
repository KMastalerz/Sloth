using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sloth.Application.Services.UIElements;
using Sloth.Domain.Entities;

namespace Sloth.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UIElementsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<WebPage?>> GetWebPage(GetWebPageQuery query)
    {
        var result = await mediator.Send(query);
        return Ok(result);
    }
}
