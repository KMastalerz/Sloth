using AutoMapper;
using Sloth.Application.DTO;
using Sloth.Domain.Entities;
using Sloth.Shared.Models;

namespace Sloth.Application.Profiles;

public class UIElementsProfile : Profile
{
    public UIElementsProfile()
    {
        CreateMap<WebPage, GetWebPage>().ReverseMap();

        CreateMap<WebPage, ListWebPageItem>().ReverseMap();

        CreateMap<WebPage, WebPageItem>().ReverseMap();

        CreateMap<WebPage, WebPageItem>()
            .ForMember(dest => dest.WebPanels, opt => opt.MapFrom(src => src.WebPanels));

        CreateMap<WebPanel, GetWebPanel>().ReverseMap();

        CreateMap<WebPanel, WebPanelItem>().ReverseMap();

        CreateMap<WebPanel, WebPanelItem>()
            .ForMember(dest => dest.WebControls, opt => opt.MapFrom(src => src.WebControls.Where(c => c.SectionID == null))) // Map controls without SectionID
            .ForMember(dest => dest.WebSections, opt => opt.MapFrom(src => src.WebSections));

        CreateMap<WebSection, GetWebSection>().ReverseMap();

        CreateMap<WebSection, WebSectionItem>().ReverseMap();

        CreateMap<WebSection, WebSectionItem>()
            .ForMember(dest => dest.WebControls, opt => opt.MapFrom(src => src.WebControls));

        CreateMap<WebControl, GetWebControl>().ReverseMap();

        CreateMap<WebControl, WebControlItem>().ReverseMap();
    }
}
