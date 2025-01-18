using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class TeamProfile : Profile
{
    public TeamProfile()
    {
        CreateMap<Team, GetTeamBugItem>().ReverseMap();
    }
}
