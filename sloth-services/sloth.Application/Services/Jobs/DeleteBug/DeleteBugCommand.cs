using MediatR;

namespace sloth.Application.Services.Jobs;
public class DeleteBugCommand(int bugID): IRequest
{
    public int BugID { get; set; } = bugID;
}
