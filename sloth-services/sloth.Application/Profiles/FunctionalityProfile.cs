using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class FunctionalityProfile : Profile
{
    public FunctionalityProfile()
    {
        CreateMap<ProductFunctionality, ListBugFunctionalityItem>()
           .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
           .ForMember(dest => dest.TagColor, opt => opt.MapFrom(src => src.TagColor));
    }
}
