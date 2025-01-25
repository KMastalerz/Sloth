using MediatR;
using sloth.Application.Models.Jobs;

namespace sloth.Application.Services.Jobs;
public class LinkJobCommand: IRequest<LinkJobItem>
{
    public int PrimaryJobID { get; set; } = default!;
    public int SecondaryJobID { get; set; } = default!;
    public string Action { get; set; } = default!;
    public string Type { get; set; } = default!;
}
