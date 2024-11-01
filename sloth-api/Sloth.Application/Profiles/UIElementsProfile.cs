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

        CreateMap<WebPanel, GetWebPanel>().ReverseMap();

        CreateMap<WebSection, GetWebSection>().ReverseMap();

        CreateMap<WebControl, GetWebControl>().ReverseMap();
    }
}
