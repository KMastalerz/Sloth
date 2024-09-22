using AutoMapper;
using Sloth.Application.DTO.Security;
using Sloth.Application.Services.Security;
using Sloth.Domain.Entities;

namespace Sloth.Application.Profiles;
internal class SecurityProfile : Profile
{
    public SecurityProfile()
    {
        CreateMap<User, RegisterCommand>().ReverseMap();

        CreateMap<User, AccessUser>().ReverseMap();
    }
}

