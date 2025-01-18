using AutoMapper;
using sloth.Application.Models.Jobs;

namespace sloth.Application.Profiles;
public class JobStatusHistoryProfile : Profile
{
    public JobStatusHistoryProfile()
    {
        CreateMap<JobStatusHistoryProfile, GetStatusHistoryBugItem>().ReverseMap();
    }
}
