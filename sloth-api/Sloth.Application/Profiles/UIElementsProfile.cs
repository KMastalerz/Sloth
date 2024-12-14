using AutoMapper;
using Sloth.Shared.DTO;
using Sloth.Domain.Entities;

namespace Sloth.Application.Profiles;

public class UIElementsProfile : Profile
{
    public UIElementsProfile()
    {
        CreateMap<WebPage, GetWebPage>().ReverseMap();

        CreateMap<WebPage, GetWebPageFull>().ReverseMap();

        CreateMap<WebPage, ListWebPageItem>().ReverseMap();

        CreateMap<WebPanel, GetWebPanel>().ReverseMap();

        CreateMap<WebPanel, GetWebPanelFull>().ReverseMap();

        CreateMap<WebSection, GetWebSection>().ReverseMap();

        CreateMap<WebSection, GetWebSectionFull>().ReverseMap();

        CreateMap<WebControl, GetWebControl>().ReverseMap();

        CreateMap<WebControl, GetWebControlFull>().ReverseMap();
    }
}
