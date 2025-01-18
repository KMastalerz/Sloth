using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class JobAssignmentProfile: Profile
{
    public JobAssignmentProfile()
    {
        CreateMap<JobAssignment, GetAssignmentBugItem>().ReverseMap();
    }
}
