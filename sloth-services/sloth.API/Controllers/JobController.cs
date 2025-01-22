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
    //[Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> DeleteBug(int bugID)
    {
        await mediator.Send(new DeleteBugCommand(bugID));
        return NoContent();
    }
    [HttpPost]
    public async Task<IActionResult> ClaimBug([FromQuery] int bugID)
    {
        var result = await mediator.Send(new ClaimBugCommand(bugID));
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> AbandonBug([FromQuery] int bugID)
    {
        var result = await mediator.Send(new AbandonBugCommand(bugID));
        return Ok(result);
    }
}
