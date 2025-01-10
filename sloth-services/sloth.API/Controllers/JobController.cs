using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sloth.Application.Services.Job;

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
}
