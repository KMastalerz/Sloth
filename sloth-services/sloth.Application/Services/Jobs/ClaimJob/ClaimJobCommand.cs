using MediatR;
using sloth.Application.Models.Jobs;

namespace sloth.Application.Services.Jobs;
public class ClaimJobCommand(int jobID) : IRequest<IEnumerable<GetAssignmentBugItem>>
{
    public int JobID { get; set; } = jobID;
}
