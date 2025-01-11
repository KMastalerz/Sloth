using MediatR;
using Microsoft.AspNetCore.Http;

namespace sloth.Application.Services.Jobs;
public class CreateQuickJobCommand: IRequest
{
    public string Type { get; set; } = default!;
    public string Header { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Priority { get; set; } = default!;
    public int[] Products { get; set; } = default!;
    public IFormFile File { get; set; } = default!;
    public bool IsClient { get; set; } = false;
    public Guid? ClientID { get; set; } = null;
}
