using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class ClientProfile: Profile
{
    public ClientProfile()
    {
        CreateMap<Client, CacheClientItem>();

        CreateMap<Client, GetClientBugItem>();
    }
}
