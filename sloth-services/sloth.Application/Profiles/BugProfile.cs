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
            // Map properties from Bug
            .ForMember(dest => dest.BugID, opt => opt.MapFrom(src => src.BugID))
            .ForMember(dest => dest.ClientInquiryNumber, opt => opt.MapFrom(src => src.InquiryNumber))
            .ForMember(dest => dest.Header, opt => opt.MapFrom(src => src.Header))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.PriorityID, opt => opt.MapFrom(src => src.PriorityID))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate))
            .ForMember(dest => dest.ClosedDate, opt => opt.MapFrom(src => src.ClosedDate))
            .ForMember(dest => dest.IsClosed, opt => opt.MapFrom(src => src.IsClosed))
            .ForMember(dest => dest.IsBlocker, opt => opt.MapFrom(src => src.IsBlocker))

            // Map IDs
            .ForMember(dest => dest.UpdatedByID, opt => opt.MapFrom(src => src.UpdatedByID))
            .ForMember(dest => dest.ClosedByID, opt => opt.MapFrom(src => src.ClosedByID))
            .ForMember(dest => dest.ClientID, opt => opt.MapFrom(src => src.ClientID))

            // Map string representations of related entities
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy != null ? $"{src.UpdatedBy.FirstName} {src.UpdatedBy.LastName}" : null))
            .ForMember(dest => dest.ClosedBy, opt => opt.MapFrom(src => src.ClosedBy != null ? $"{src.ClosedBy.FirstName} {src.ClosedBy.LastName}" : null))
            .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client != null ? src.Client.Name : null));
    }
}
