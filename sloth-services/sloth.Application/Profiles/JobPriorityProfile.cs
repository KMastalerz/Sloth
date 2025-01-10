using AutoMapper;
using sloth.Application.Models.Miscellaneous;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class JobPriorityProfile : Profile
{
    public JobPriorityProfile()
    {
        CreateMap<JobPriority, ToggleItem>()
           .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Priority))
           .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Priority))
           .ForMember(dest => dest.Class, opt => opt.MapFrom(src => src.PriorityClass)); ;
    }
}
