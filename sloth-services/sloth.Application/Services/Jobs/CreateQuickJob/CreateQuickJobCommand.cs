using MediatR;
using Microsoft.AspNetCore.Http;

namespace sloth.Application.Services.Jobs;
public class CreateQuickJobCommand: IRequest
{
    public string Type { get; set; } = default!;
    public string Header { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int? StatusID { get; set; } = null;
    public int PriorityID { get; set; }
    public int[] Products { get; set; } = [];
    public int[]? Functionalities { get; set; } = null;
    public IFormFile[]? Files { get; set; } = null;
    public Guid? ClientID { get; set; } = null;
    public DateTime? RaisedDate { get; set; } = null;
}
