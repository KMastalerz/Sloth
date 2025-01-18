using AutoMapper;
using sloth.Application.Models.Jobs;

namespace sloth.Application.Profiles;
public class JobAssignmentHistoryProfile : Profile
{
    public JobAssignmentHistoryProfile()
    {
        CreateMap<JobAssignmentHistoryProfile, GetAssignmentHistoryBugItem>().ReverseMap();
    }
}
