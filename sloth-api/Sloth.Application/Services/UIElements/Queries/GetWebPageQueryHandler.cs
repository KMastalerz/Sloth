using MediatR;
using Microsoft.Extensions.Logging;
using Sloth.Application.UserIdentity;
using Sloth.Domain.Constants;
using Sloth.Application.DTO;
using Sloth.Domain.Entities;
using Sloth.Domain.Exceptions;
using Sloth.Domain.Repositories;
using AutoMapper;

namespace Sloth.Application.Services.UIElements;
public class GetWebPageQueryHandler(ILogger<GetWebPageQueryHandler> logger, IUserContext userContext, IUIElementsRepository uIElementsRepository, IMapper mapper) : IRequestHandler<GetWebPageQuery, GetWebPageDTO?>
{
    public async Task<GetWebPageDTO?> Handle(GetWebPageQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("User: {UserName} is trying to access the page {PageID}", currentUser!.UserName, request.PageID);

        if (currentUser?.UserGroup is null)
            throw new NotFoundException(nameof(UserClaim), SlothClaims.UserGroup);

        var webPage = await uIElementsRepository.GetWebPageAsync(request.PageID, currentUser.UserGroup) ??
            throw new NotFoundException(nameof(WebPage), request.PageID);

        var webControls = await uIElementsRepository.ListWebControlAsync(request.PageID, currentUser.UserGroup) ?? [];

        var secTables = await uIElementsRepository.ListControlSecurityAsync(request.PageID, currentUser.UserGroup, webPage.SecurityTableID) ?? [];

        // Join the two lists on a common property (Id)
        var joinedList = from wc in webControls
                         join st in secTables on wc.ControlID equals st.ControlID into secGroup
                         from st in secGroup.DefaultIfEmpty()
                         select new { wc, st };

        // use mapper on joined list to map to DTO
        var dtoControls = joinedList.Select(jl => {
            var dto = mapper.Map<GetWebControlDTO>(jl.wc);
            mapper.Map(jl.st, dto);
            return dto;
        }).ToList();

        // TO DO: Add translation to the controls

        var dto = mapper.Map<GetWebPageDTO>(webPage);
        dto.WebControls = dtoControls;

        return dto;
    }
}
