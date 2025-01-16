using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Application.Models.Miscellaneous;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class PriorityProfile : Profile
{
    public PriorityProfile()
    {
        CreateMap<Priority, ToggleItem>()
           .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.PriorityID))
           .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Tag))
           .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.TagColor));

        CreateMap<Priority, ListBugPriorityItem>()
           .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag))
           .ForMember(dest => dest.TagColor, opt => opt.MapFrom(src => src.TagColor));
    }
}
