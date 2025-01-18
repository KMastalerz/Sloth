using AutoMapper;
using MediatR;
using sloth.Application.Models.Jobs;
using sloth.Domain.Repositories;

namespace sloth.Application.Services.Jobs;
public class GetBugQueryHandler(
    IMapper mapper,
    IJobRepository jobRepository)
    : IRequestHandler<GetBugQuery, GetBugItem>
{
    public async Task<GetBugItem> Handle(GetBugQuery request, CancellationToken cancellationToken)
    {
        var bug = await jobRepository.GetBugAsync(request.BugID);
        var result = mapper.Map<GetBugItem>(bug);
        return result;
    }
}
