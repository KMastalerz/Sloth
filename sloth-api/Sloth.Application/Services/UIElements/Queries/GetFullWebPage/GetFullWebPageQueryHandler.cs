using AutoMapper;
using MediatR;
using Sloth.Domain.Repositories;
using Sloth.Shared.Models;

namespace Sloth.Application.Services.UIElements;
public class GetFullWebPageQueryHandler(IUIElementsRepository uIElementsRepository, IMapper mapper) : IRequestHandler<GetFullWebPageQuery, WebPageItem>
{
    public async Task<WebPageItem> Handle(GetFullWebPageQuery request, CancellationToken cancellationToken)
    {
        var webPage = await uIElementsRepository.GetFullWebPageAsync(request.AppID, request.PageID);
       
        var result = mapper.Map<WebPageItem>(webPage);

        return result;
    }
}
