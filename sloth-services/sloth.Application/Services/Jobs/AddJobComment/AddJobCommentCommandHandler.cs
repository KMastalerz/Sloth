using AutoMapper;
using MediatR;
using sloth.Application.Models.Jobs;
using sloth.Application.UserIdentity;
using sloth.Domain.Entities;
using sloth.Domain.Exceptions;
using sloth.Domain.Repositories;

namespace sloth.Application.Services.Jobs;
public class AddJobCommentCommandHandler(
    IMapper mapper,
    IJobRepository jobRepository,
    IUserContext userContext) 
    : IRequestHandler<AddJobCommentCommand, IEnumerable<GetCommentBugItem>>
{
    public async Task<IEnumerable<GetCommentBugItem>> Handle(AddJobCommentCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser() ??
            throw new MissingUserContextException();

        var newComment = new JobComment()
        {
            JobID = request.JobID,
            CommentedByID = user.UserGuid,
            Comment = request.Comment,
        };

        await jobRepository.AddJobCommentAsync(newComment);

        var comments = await jobRepository.ListJobCommentsAsync(request.JobID);

        var results = mapper.Map<IEnumerable<GetCommentBugItem>>(comments);

        return results;
    }
}
