using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class JobCommentProfile: Profile
{
    public JobCommentProfile()
    {
        CreateMap<JobComment, GetCommentBugItem>().ReverseMap();
    }
}
