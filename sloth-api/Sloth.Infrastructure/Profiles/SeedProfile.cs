using AutoMapper;
using Sloth.Domain.Entities;
using Sloth.Infrastructure.Seed;

namespace Sloth.Infrastructure.Profiles;
internal class SeedProfile : Profile
{
    public SeedProfile()
    {
        CreateMap<User, UserSeed>().ReverseMap();
    }
}
