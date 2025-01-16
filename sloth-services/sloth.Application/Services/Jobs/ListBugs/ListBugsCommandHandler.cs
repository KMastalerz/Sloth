using AutoMapper;
using MediatR;
using sloth.Application.Models.Jobs;
using sloth.Domain.Repositories;

namespace sloth.Application.Services.Jobs;
public class ListBugsCommandHandler(
    IMapper mapper,
    IJobRepository jobRepository
    ) : IRequestHandler<ListBugsCommand, IEnumerable<ListBugItem>>
{
    public async Task<IEnumerable<ListBugItem>> Handle(ListBugsCommand request, CancellationToken cancellationToken)
    {
        var results = await jobRepository.ListBugsAsync(request);
        return mapper.Map<IEnumerable<ListBugItem>>(results);
    }
}
