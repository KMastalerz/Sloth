using AutoMapper;
using sloth.Application.Services.Auth;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, RegisterCommand>().ReverseMap();
    }
}
