using AutoMapper;
using Sloth.Application.DTO;
using Sloth.Domain.Entities;

namespace Sloth.Application.Profiles;
public class UIElements : Profile
{
    public UIElements()
    {
        CreateMap<WebPage, GetWebPageDTO>();
        CreateMap<WebControl, GetWebControlDTO>();
    }
}
