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
    public async Task<IActionResult> ListProductsWithClientID([FromQuery] Guid? clientID = null)
    {
        var result = await mediator.Send(new ListProductsWithClientIDQuery(clientID));
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> ListFunctionalitiesWithProductID([FromQuery] IEnumerable<int>? productIDs = null)
    {
        var result = await mediator.Send(new ListFunctionalitiesWithProductIDQuery(productIDs));
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> ListBugs([FromQuery] ListBugsQuery command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetBug(int bugID)
    {
        var result = await mediator.Send(new GetBugQuery(bugID));
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> AddJobComment(AddJobCommentCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
}
