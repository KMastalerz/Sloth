using AutoMapper;
using Sloth.Application.Services.Security;
using Sloth.Domain.Entities;

namespace Sloth.Application.Profiles;
public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<RegisterCommand, User>();
    }
}
