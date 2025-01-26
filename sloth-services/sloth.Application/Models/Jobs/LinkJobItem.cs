namespace sloth.Application.Models.Jobs;
public class LinkJobItem
{
    public IEnumerable<GetParentJobLinkBugItem> ParentJobs { get; set; } = [];
    public IEnumerable<GetChildJobLinkBugItem> ChildJobs { get; set; } = [];
}
