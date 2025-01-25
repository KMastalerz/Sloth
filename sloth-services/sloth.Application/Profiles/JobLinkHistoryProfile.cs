using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class JobLinkHistoryProfile : Profile
{
    public JobLinkHistoryProfile()
    {
        CreateMap<JobLinkHistory, GetParentJobLinkHistoryBugItem>()
            .ForMember(dest => dest.JobID, opt => opt.MapFrom(src => src.ParentJob.JobID))
            .ForMember(dest => dest.JobHeader, opt => opt.MapFrom(src => src.ParentJob.Header))
            .ForMember(dest => dest.JobDescription, opt => opt.MapFrom(src => src.ParentJob.Description))
            .ForMember(dest => dest.ChangedBy, opt => opt.MapFrom(src => src.ChangedBy.UserName))
            .ForMember(dest => dest.ChangedByFullName, opt => opt.MapFrom(src => $"{src.ChangedBy.FirstName} {src.ChangedBy.LastName}"))
            .ForMember(dest => dest.ChangedByEmail, opt => opt.MapFrom(src => src.ChangedBy.Email));

        CreateMap<JobLinkHistory, GetChildJobLinkHistoryBugItem>()
            .ForMember(dest => dest.JobID, opt => opt.MapFrom(src => src.ChildJob.JobID))
            .ForMember(dest => dest.JobHeader, opt => opt.MapFrom(src => src.ChildJob.Header))
            .ForMember(dest => dest.JobDescription, opt => opt.MapFrom(src => src.ChildJob.Description))
            .ForMember(dest => dest.ChangedBy, opt => opt.MapFrom(src => src.ChangedBy.UserName))
            .ForMember(dest => dest.ChangedByFullName, opt => opt.MapFrom(src => $"{src.ChangedBy.FirstName} {src.ChangedBy.LastName}"))
            .ForMember(dest => dest.ChangedByEmail, opt => opt.MapFrom(src => src.ChangedBy.Email));
    }
}
