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
           .ForMember(dest => dest.PriorityID, opt => opt.MapFrom(src => src.PriorityID))
           .ForMember(dest => dest.PriorityTag, opt => opt.MapFrom(src => src.Priority != null ? src.Priority.Tag : null))
           .ForMember(dest => dest.PriorityTagColor, opt => opt.MapFrom(src => src.Priority != null ? src.Priority.TagColor : null))
           .ForMember(dest => dest.PriorityDescription, opt => opt.MapFrom(src => src.Priority != null ? src.Priority.Description : null));
    }
}
