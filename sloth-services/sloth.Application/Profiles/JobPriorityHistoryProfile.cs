using AutoMapper;
using sloth.Application.Models.Jobs;

namespace sloth.Application.Profiles;
public class JobPriorityHistoryProfile : Profile
{
    public JobPriorityHistoryProfile()
    {
        CreateMap<JobPriorityHistoryProfile, GetPriorityHistoryBugItem>().ReverseMap();
    }
}
