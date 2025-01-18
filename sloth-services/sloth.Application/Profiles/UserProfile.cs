using AutoMapper;
using sloth.Application.Models.Auth;
using sloth.Application.Models.Jobs;
using sloth.Application.Services.Auth;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, RegisterCommand>().ReverseMap();

        CreateMap<User, AccessUser>();

        CreateMap<User, GetUserBugItem>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
    }
}
