using AutoMapper;
using Sloth.Domain.Entities;
using Sloth.Infrastructure.Seed;

namespace Sloth.Infrastructure.Profiles;
internal class SeedProfile : Profile
{
    public SeedProfile()
    {
        CreateMap<Language, LanguageSeed>().ReverseMap();

        CreateMap<SystemOption, SystemOptionSeed>().ReverseMap();

        CreateMap<UserRole, UserRoleSeed>().ReverseMap();

        CreateMap<UserGroup, UserGroupSeed>().ReverseMap();

        CreateMap<User, UserSeed>().ReverseMap();

        CreateMap<WebPage, WebPageSeed>().ReverseMap();

        CreateMap<WebPageSecurity, WebPageSecuritySeed>().ReverseMap();

        CreateMap<WebPanel, WebPanelSeed>().ReverseMap();

        CreateMap<WebSection, WebSectionSeed>().ReverseMap();

        CreateMap<WebControl, WebControlSeed>().ReverseMap();
    }
}
