using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class JobAssignmentHistoryProfile : Profile
{
    public JobAssignmentHistoryProfile()
    {
        CreateMap<JobAssignmentHistory, GetAssignmentHistoryBugItem>()
            .ForMember(dest => dest.CurrentOwner, opt => opt.MapFrom(src => src.CurrentOwner.UserName))
            .ForMember(dest => dest.CurrentOwnerFullName, opt => opt.MapFrom(src => $"{src.CurrentOwner.FirstName} {src.CurrentOwner.LastName}"))
            .ForMember(dest => dest.CurrentOwnerEmail, opt => opt.MapFrom(src => src.CurrentOwner.Email))
            .ForMember(dest => dest.PreviousOwner, opt => opt.MapFrom(src => src.PreviousOwner.UserName))
            .ForMember(dest => dest.PreviousOwnerFullName, opt => opt.MapFrom(src => $"{src.PreviousOwner.FirstName} {src.PreviousOwner.LastName}"))
            .ForMember(dest => dest.PreviousOwnerEmail, opt => opt.MapFrom(src => src.PreviousOwner.Email))
            .ForMember(dest => dest.ChangedBy, opt => opt.MapFrom(src => src.ChangedBy.UserName))
            .ForMember(dest => dest.ChangedByFullName, opt => opt.MapFrom(src => $"{src.ChangedBy.FirstName} {src.ChangedBy.LastName}"))
            .ForMember(dest => dest.ChangedByEmail, opt => opt.MapFrom(src => src.ChangedBy.Email))
            .ForMember(dest => dest.Team, opt => opt.MapFrom(src => src.Team.Name));
    }
}
