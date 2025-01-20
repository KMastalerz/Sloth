using MediatR;
using sloth.Application.Models.Jobs;

namespace sloth.Application.Services.Jobs;
public class ListBugDataCacheQuery(int bugID): IRequest<ListJobDataCacheItem>
{
    public int BugID { get; set; } = bugID;
}
