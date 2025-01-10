using AutoMapper;
using sloth.Application.Models.Auth;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class UserRoleProfile : Profile
{
    public UserRoleProfile()
    {
        CreateMap<UserRole, AccessRole>();
    }
   
}
