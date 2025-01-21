using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class StatusProfile : Profile
{
    public StatusProfile()
    {
        CreateMap<Status, ListBugStatusItem>();

        CreateMap<Status, CacheStatusItem>();

        CreateMap<Status, GetStatusBugItem>();
    }
}
