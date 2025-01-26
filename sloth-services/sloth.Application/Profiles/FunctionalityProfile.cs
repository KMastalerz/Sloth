using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class FunctionalityProfile : Profile
{
    public FunctionalityProfile()
    {
        CreateMap<ProductFunctionality, ListBugFunctionalityItem>();

        CreateMap<ProductFunctionality, CacheFunctionalityItem>()
           .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : null));

        CreateMap<ProductFunctionality, GetFunctionalityBugItem>();
    }
}
