using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Application.Models.Miscellaneous;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class ClientProfile: Profile
{
    public ClientProfile()
    {
        CreateMap<Client, ListItem>()
           .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ClientID))
           .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name));

        CreateMap<Client, GetClientBugItem>().ReverseMap();
    }
}
