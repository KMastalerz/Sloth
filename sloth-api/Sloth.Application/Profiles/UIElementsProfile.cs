using AutoMapper;
using Newtonsoft.Json;
using Sloth.Application.DTO;
using Sloth.Domain.Entities;

namespace Sloth.Application.Profiles;

public class UIElementsProfile : Profile
{
    public UIElementsProfile()
    {
        CreateMap<WebPage, GetWebPage>()
            .ForMember(dest => dest.Panels, opt => opt.MapFrom(
                src => !string.IsNullOrEmpty(src.Panels) ? JsonConvert.DeserializeObject<List<string>>(src.Panels) : null))
            .ReverseMap()
            .ForMember(dest => dest.Panels, opt => opt.MapFrom(
                src => src.Panels != null ? JsonConvert.SerializeObject(src.Panels) : null));

        CreateMap<WebPanel, GetWebPanel>()
            .ForMember(dest => dest.Sections, opt => opt.MapFrom(
                src => !string.IsNullOrEmpty(src.Sections) ? JsonConvert.DeserializeObject<List<string>>(src.Sections) : null))
            .ReverseMap()
            .ForMember(dest => dest.Sections, opt => opt.MapFrom(
                src => src.Sections != null ? JsonConvert.SerializeObject(src.Sections) : null));

        CreateMap<WebSection, GetWebSection>()
            .ForMember(dest => dest.Controls, opt => opt.MapFrom(
                src => !string.IsNullOrEmpty(src.Controls) ? JsonConvert.DeserializeObject<List<string>>(src.Controls) : null))
            .ReverseMap()
            .ForMember(dest => dest.Controls, opt => opt.MapFrom(
                src => src.Controls != null ? JsonConvert.SerializeObject(src.Controls) : null));

        CreateMap<WebControl, GetWebControl>().ReverseMap();
    }
}
