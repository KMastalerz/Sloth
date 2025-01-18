using MediatR;
using sloth.Application.Models.Jobs;

namespace sloth.Application.Services.Jobs;
public class AddJobCommentCommand : IRequest<IEnumerable<GetCommentBugItem>>
{
    public int JobID { get; set; }
    public string Comment { get; set; } = default!;
}
