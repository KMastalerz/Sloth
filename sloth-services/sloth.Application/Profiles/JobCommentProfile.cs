using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class JobCommentProfile: Profile
{
    public JobCommentProfile()
    {
        CreateMap<JobComment, GetCommentBugItem>()
           .ForMember(dest => dest.CommentedBy, opt => opt.MapFrom(src => src.CommentedBy.UserName))
           .ForMember(dest => dest.CommentedByEmail, opt => opt.MapFrom(src => src.CommentedBy.Email))
           .ForMember(dest => dest.CommentedByFullName, opt => opt.MapFrom(src => $"{src.CommentedBy.FirstName} {src.CommentedBy.LastName}"));
    }
}
