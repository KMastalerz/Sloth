using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class JobLinkProfile : Profile
{
    public JobLinkProfile()
    {
        CreateMap<JobLink, GetParentJobLinkBugItem>()
            .ForMember(dest => dest.JobID, opt => opt.MapFrom(src => src.ParentJob.JobID))
            .ForMember(dest => dest.JobHeader, opt => opt.MapFrom(src => src.ParentJob.Header))
            .ForMember(dest => dest.JobDescription, opt => opt.MapFrom(src => src.ParentJob.Description))
            .ForMember(dest => dest.LinkedBy, opt => opt.MapFrom(src => src.LinkedBy.UserName))
            .ForMember(dest => dest.LinkedByFullName, opt => opt.MapFrom(src => $"{src.LinkedBy.FirstName} {src.LinkedBy.LastName}"))
            .ForMember(dest => dest.LinkedByEmail, opt => opt.MapFrom(src => src.LinkedBy.Email));

        CreateMap<JobLink, GetChildJobLinkBugItem>()
            .ForMember(dest => dest.JobID, opt => opt.MapFrom(src => src.ChildJob.JobID))
            .ForMember(dest => dest.JobHeader, opt => opt.MapFrom(src => src.ChildJob.Header))
            .ForMember(dest => dest.JobDescription, opt => opt.MapFrom(src => src.ChildJob.Description))
            .ForMember(dest => dest.LinkedBy, opt => opt.MapFrom(src => src.LinkedBy.UserName))
            .ForMember(dest => dest.LinkedByFullName, opt => opt.MapFrom(src => $"{src.LinkedBy.FirstName} {src.LinkedBy.LastName}"))
            .ForMember(dest => dest.LinkedByEmail, opt => opt.MapFrom(src => src.LinkedBy.Email));
    }
}
