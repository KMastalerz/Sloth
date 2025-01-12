using AutoMapper;
using sloth.Application.Models.Miscellaneous;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class PriorityProfile : Profile
{
    public PriorityProfile()
    {
        CreateMap<Priority, ToggleItem>()
           .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.PriorityID))
           .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.PriorityValue))
           .ForMember(dest => dest.Class, opt => opt.MapFrom(src => src.Class)); ;
    }
}
