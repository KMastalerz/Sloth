using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, CacheProductItem>();

        CreateMap<Product, ListBugProductItem>();

        CreateMap<Product, GetProductBugItem>();
    }
}
