using MediatR;
using sloth.Application.Models.Jobs;

namespace sloth.Application.Services.Jobs;
public class GetBugQuery(int bugID): IRequest<GetBugItem>
{
    public int BugID { get; set; } = bugID;
}
