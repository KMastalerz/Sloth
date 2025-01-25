using FluentValidation;
using sloth.Application.UserIdentity;
using sloth.Domain.Repositories;

namespace sloth.Application.Services.Jobs;
public class LinkJobCommandValidation : AbstractValidator<LinkJobCommand>
{
    public LinkJobCommandValidation(IJobRepository jobRepository, IUserContext userContext)
    {
        // Job 1 is parent to job 2
        // Job 1 is parent to job 3
        // Job 2 and job 3 can be siblings but cannot be in ancestory relationship
        // Job 3 is sibling to job 4, therefore job 4 cannot be direct ancestory to job 1 or 2
    }
}
