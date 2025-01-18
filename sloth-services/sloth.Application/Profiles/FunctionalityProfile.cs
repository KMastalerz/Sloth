using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Application.Models.Miscellaneous;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class FunctionalityProfile : Profile
{
    public FunctionalityProfile()
    {
        CreateMap<ProductFunctionality, ListBugFunctionalityItem>().ReverseMap();

        CreateMap<ProductFunctionality, ListItem>()
           .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.FunctionalityID))
           .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : null));

        CreateMap<ProductFunctionality, GetFunctionalityBugItem>().ReverseMap();
    }
}
