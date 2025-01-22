using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class JobPriorityHistoryProfile : Profile
{
    public JobPriorityHistoryProfile()
    {
        CreateMap<JobPriorityHistory, GetPriorityHistoryBugItem>()
           .ForMember(dest => dest.ChangedBy, opt => opt.MapFrom(src =>  src.ChangedBy.UserName))
           .ForMember(dest => dest.ChangedByEmail, opt => opt.MapFrom(src => src.ChangedBy.Email))
           .ForMember(dest => dest.ChangedByFullName, opt => opt.MapFrom(src => $"{src.ChangedBy.FirstName} {src.ChangedBy.LastName}"))
           .ForMember(dest => dest.NewPriorityID, opt => opt.MapFrom(src => src.NewPriority.PriorityID))
           .ForMember(dest => dest.NewPriorityTag, opt => opt.MapFrom(src => src.NewPriority.Tag))
           .ForMember(dest => dest.NewPriorityTagColor, opt => opt.MapFrom(src => src.NewPriority.TagColor))
           .ForMember(dest => dest.NewPriorityDescription, opt => opt.MapFrom(src => src.NewPriority.Description));
    }
}
