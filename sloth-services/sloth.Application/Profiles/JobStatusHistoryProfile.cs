using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class JobStatusHistoryProfile : Profile
{
    public JobStatusHistoryProfile()
    {
        CreateMap<JobStatusHistory, GetStatusHistoryBugItem>()
           .ForMember(dest => dest.ChangedBy, opt => opt.MapFrom(src => src.ChangedBy.UserName))
           .ForMember(dest => dest.ChangedByEmail, opt => opt.MapFrom(src => src.ChangedBy.Email))
           .ForMember(dest => dest.ChangedByFullName, opt => opt.MapFrom(src => $"{src.ChangedBy.FirstName} {src.ChangedBy.LastName}"))
           .ForMember(dest => dest.StatusID, opt => opt.MapFrom(src => src.StatusID))
           .ForMember(dest => dest.StatusTag, opt => opt.MapFrom(src => src.Status != null ? src.Status.Tag : null))
           .ForMember(dest => dest.StatusTagColor, opt => opt.MapFrom(src => src.Status != null ? src.Status.TagColor : null))
           .ForMember(dest => dest.StatusDescription, opt => opt.MapFrom(src => src.Status != null ? src.Status.Description : null));

    }
}
