using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class JobAssignmentHistoryProfile : Profile
{
    public JobAssignmentHistoryProfile()
    {
        CreateMap<JobAssignmentHistory, GetAssignmentHistoryBugItem>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => $"{src.User} {src.User.LastName}"))
            .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.ChangedBy, opt => opt.MapFrom(src => src.ChangedBy.UserName))
            .ForMember(dest => dest.ChangedByFullName, opt => opt.MapFrom(src => $"{src.ChangedBy.FirstName} {src.ChangedBy.LastName}"))
            .ForMember(dest => dest.ChangedByEmail, opt => opt.MapFrom(src => src.ChangedBy.Email));
    }
}
