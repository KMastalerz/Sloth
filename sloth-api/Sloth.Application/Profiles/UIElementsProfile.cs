using AutoMapper;
using Sloth.Application.DTO;
using Sloth.Domain.Entities;

namespace Sloth.Application.Profiles;

public class UIElementsProfile : Profile
{
    public UIElementsProfile()
    {
        CreateMap<WebPage, GetWebPage>().ReverseMap();

        CreateMap<WebPage, DesignerListWebPageItem>().ReverseMap();

        CreateMap<WebPanel, GetWebPanel>().ReverseMap();

        CreateMap<WebSection, GetWebSection>().ReverseMap();

        CreateMap<WebControl, GetWebControl>().ReverseMap();
    }
}
