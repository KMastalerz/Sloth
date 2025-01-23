using MediatR;
using sloth.Application.Models.Jobs;

namespace sloth.Application.Services.Jobs;
public class SaveBugCommand : IRequest<GetBugItem>
{
    public int BugID { get; set; }
    public bool ClientChanged { get; set; }
    public Guid? NewClientID { get; set; } = null;
    public bool PriorityChanged { get; set; }
    public int? NewPriorityID { get; set; } = null;
    public bool StatusChanged { get; set; }
    public int? NewStatusID { get; set; } = null;
    public IEnumerable<int> NewFunctionalityIDs { get; set; } = [];
    public IEnumerable<int> RemovedFunctionalityIDs { get; set; } = [];
    public IEnumerable<int> NewProductIDs { get; set; } = [];
    public IEnumerable<int> RemovedProductIDs { get; set; } = [];
    public string? NewHeader { get; set; } = null;
    public string? NewDescription { get; set; } = null;
}
