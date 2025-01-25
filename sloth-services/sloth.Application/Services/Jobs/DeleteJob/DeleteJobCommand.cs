using MediatR;

namespace sloth.Application.Services.Jobs;
public class DeleteJobCommand(int jobID): IRequest
{
    public int JobID { get; set; } = jobID;
}
