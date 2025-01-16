using sloth.Domain.Entities;

namespace sloth.Domain.Models.Jobs;
public class ListBugItemRepositoryResponse
{
    public int TotalCount { get; set; }
    public IEnumerable<Bug> Bugs { get; set; } = [];
}
