using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sloth.Application.Services.Jobs;

namespace sloth.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
public class JobController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ListJobDataCache()
    {
        var result = await mediator.Send(new ListJobDataCacheQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateQuickJob(CreateQuickJobCommand command)
    {
        await mediator.Send(command);
        return Created();
    }
    [HttpGet]
    public async Task<IActionResult> ListProductsWithClientID(Guid? clientID = null)
    {
        var result = await mediator.Send(new ListProductsWithClientIDCommand(clientID));
        return Ok(result);
    }
}
