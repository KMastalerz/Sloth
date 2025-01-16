using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Application.Models.Miscellaneous;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ListItem>()
           .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ProductID))
           .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name));

        CreateMap<Product, ListBugProductItem>()
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}
