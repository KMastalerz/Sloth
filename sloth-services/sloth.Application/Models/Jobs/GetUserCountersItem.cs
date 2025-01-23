namespace sloth.Application.Models.Jobs;
public class GetUserCountersItem(int bugCount = 0)
{
    public int BugCount { get; set; } = bugCount;
}
