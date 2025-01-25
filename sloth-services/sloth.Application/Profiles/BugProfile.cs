using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Application.Services.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class BugProfile : Profile
{
    public BugProfile()
    {
        CreateMap<Bug, GetBugItem>().ReverseMap();

        CreateMap<CreateJobCommand, Bug>();

        CreateMap<Bug, ListBugItem>()
            // Map string representations of related entities
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy != null ? $"{src.UpdatedBy.FirstName} {src.UpdatedBy.LastName}" : null))
            .ForMember(dest => dest.ClosedBy, opt => opt.MapFrom(src => src.ClosedBy != null ? $"{src.ClosedBy.FirstName} {src.ClosedBy.LastName}" : null))
            .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client != null ? src.Client.Name : null));
    }
}
