using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class StatusProfile : Profile
{
    public StatusProfile()
    {
        CreateMap<Status, ListBugStatusItem>()
           .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag))
           .ForMember(dest => dest.TagColor, opt => opt.MapFrom(src => src.TagColor));

        CreateMap<Status, GetStatusBugItem>().ReverseMap();
    }
}
