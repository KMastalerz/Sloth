using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sloth.Application.Services.Jobs;
using sloth.Domain.Constants;
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
    public async Task<IActionResult> CreateJob(CreateJobCommand command)
    {
        await mediator.Send(command);
        return Created();
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
    [HttpDelete]
    [Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> DeleteJob(int jobID)
    {
        await mediator.Send(new DeleteJobCommand(jobID));
        return NoContent();
    }
    [HttpPost]
    public async Task<IActionResult> ClaimJob([FromQuery] int jobID)
    {
        var result = await mediator.Send(new ClaimJobCommand(jobID));
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> AbandonJob([FromQuery] int jobID)
    {
        var result = await mediator.Send(new AbandonJobCommand(jobID));
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> SaveBug(SaveBugCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetUserCounters()
    {
        var result = await mediator.Send(new GetUserCountersQuery());
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> LinkJob(LinkJobCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
}
