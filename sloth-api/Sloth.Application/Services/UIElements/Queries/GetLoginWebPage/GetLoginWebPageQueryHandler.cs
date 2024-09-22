using AutoMapper;
using MediatR;
using Sloth.Application.DTO;
using Sloth.Domain.Entities;
using Sloth.Domain.Exceptions;
using Sloth.Domain.Repositories;

namespace Sloth.Application.Services.UIElements;
public class GetLoginWebPageQueryHandler(IMapper mapper, IUIElementsRepository uIElementsRepository) : IRequestHandler<GetLoginWebPageQuery, GetWebPage>
{
    public async Task<GetWebPage> Handle(GetLoginWebPageQuery request, CancellationToken cancellationToken)
    {
        var webPage = await uIElementsRepository.GetLoginWebPageAsync(request.PageID) ??
            throw new NotFoundException(nameof(WebPage), request.PageID);

        var webControls = await uIElementsRepository.ListWebControlAsync(request.PageID) ??
            throw new NotFoundException(nameof(List<WebControl>), request.PageID);

        // TO DO: Add translation to the controls
        var dto = mapper.Map<GetWebPage>(webPage);
        dto.WebControls = mapper.Map<List<GetWebControl>>(webControls);

        return dto;
    }
}
