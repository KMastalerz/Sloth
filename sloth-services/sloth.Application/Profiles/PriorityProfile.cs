using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class PriorityProfile : Profile
{
    public PriorityProfile()
    {
        CreateMap<Priority, CachePriorityItem>();

        CreateMap<Priority, ListBugPriorityItem>();

        CreateMap<Priority, GetPriorityBugItem>();
    }
}
