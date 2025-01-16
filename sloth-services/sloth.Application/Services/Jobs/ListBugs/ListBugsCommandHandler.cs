using AutoMapper;
using MediatR;
using sloth.Application.Models.Jobs;
using sloth.Domain.Repositories;

namespace sloth.Application.Services.Jobs;
public class ListBugsCommandHandler(
    IMapper mapper,
    IJobRepository jobRepository
    ) : IRequestHandler<ListBugsCommand, ListBugItemResponse>
{
    public async Task<ListBugItemResponse> Handle(ListBugsCommand request, CancellationToken cancellationToken)
    {
        var results = await jobRepository.ListBugsAsync(request);
        var bugs = mapper.Map<IEnumerable<ListBugItem>>(results.Bugs);
        return new()
        {
            TotalCount = results.TotalCount,
            Bugs = bugs
        };
    }
}
