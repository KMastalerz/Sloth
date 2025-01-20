using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class JobAssignmentProfile: Profile
{
    public JobAssignmentProfile()
    {
        CreateMap<JobAssignment, GetAssignmentBugItem>()
           .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src => src.User.UserName))
           .ForMember(dest => dest.AssignedToEmail, opt => opt.MapFrom(src => src.User.Email))
           .ForMember(dest => dest.AssignedToFullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
           .ForMember(dest => dest.AssignedBy, opt => opt.MapFrom(src => src.AssignedBy.UserName))
           .ForMember(dest => dest.AssignedByEmail, opt => opt.MapFrom(src => src.AssignedBy.Email))
           .ForMember(dest => dest.AssignedByFullName, opt => opt.MapFrom(src => $"{src.AssignedBy.FirstName} {src.AssignedBy.LastName}"))
           .ForMember(dest => dest.Team, opt => opt.MapFrom(src => src.Team.Name));
    }
}
