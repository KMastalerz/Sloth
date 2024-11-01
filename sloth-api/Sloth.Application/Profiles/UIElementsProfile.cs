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

        CreateMap<WebPage, WebPageItem>().ReverseMap();

        CreateMap<WebPage, ListWebPageItem>().ReverseMap();

        CreateMap<WebPanel, GetWebPanel>().ReverseMap();

        CreateMap<WebPanel, WebPanelItem>().ReverseMap();

        CreateMap<WebSection, GetWebSection>().ReverseMap();

        CreateMap<WebSection, WebSectionItem>().ReverseMap();

        CreateMap<WebControl, GetWebControl>().ReverseMap();

        CreateMap<WebControl, WebControlItem>().ReverseMap();
    }
}
