using MediatR;
using sloth.Application.Models.Jobs;
using sloth.Application.UserIdentity;
using sloth.Domain.Exceptions;
using sloth.Domain.Repositories;

namespace sloth.Application.Services.Jobs;
public class GetUserCountersQueryHandler(
    IUserContext userContext,
    IJobRepository jobRepository) : IRequestHandler<GetUserCountersQuery, GetUserCountersItem>
{
    public async Task<GetUserCountersItem> Handle(GetUserCountersQuery request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser()
            ?? throw new InvalidUserException();

        var bugCount = await jobRepository.GetBugCountByUserAsync(user.UserGuid);

        var result = new GetUserCountersItem(bugCount);

        return result;
    }
}
