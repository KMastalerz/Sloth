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
           .ForMember(dest => dest.NewStatusID, opt => opt.MapFrom(src => src.NewStatusID))
           .ForMember(dest => dest.NewStatusTag, opt => opt.MapFrom(src => src.NewStatus != null ? src.NewStatus.Tag : null))
           .ForMember(dest => dest.NewStatusTagColor, opt => opt.MapFrom(src => src.NewStatus != null ? src.NewStatus.TagColor : null))
           .ForMember(dest => dest.NewStatusDescription, opt => opt.MapFrom(src => src.NewStatus != null ? src.NewStatus.Description : null));

    }
}
