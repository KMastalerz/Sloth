using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class JobFileProfile : Profile
{
    public JobFileProfile()
    {
        CreateMap<JobFile, GetFileBugItem>()
           .ForMember(dest => dest.AddedBy, opt => opt.MapFrom(src => src.AddedBy.UserName))
           .ForMember(dest => dest.AddedByEmail, opt => opt.MapFrom(src => src.AddedBy.Email))
           .ForMember(dest => dest.AddedByFullName, opt => opt.MapFrom(src => $"{src.AddedBy.FirstName} {src.AddedBy.LastName}"));
    }
}
