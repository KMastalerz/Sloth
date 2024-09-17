using MediatR;
using Microsoft.Extensions.Logging;
using Sloth.Application.UserIdentity;
using Sloth.Domain.Entities;
using Sloth.Domain.Exceptions;
using Sloth.Domain.Repositories;

namespace Sloth.Application.Services.UIElements;
public class GetWebPageQueryHandler(ILogger<GetWebPageQueryHandler> logger, IUserContext userContext, IUIElementsRepository uIElementsRepository) : IRequestHandler<GetWebPageQuery, WebPage?>
{
    public async Task<WebPage?> Handle(GetWebPageQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        logger.LogInformation("{UserName} tries to access page {PageID}", currentUser!.UserName, request.PageID);

        var webPage = await uIElementsRepository.GetWebPageAsync(request.PageID) ?? 
            throw new NotFoundException(nameof(WebPage), request.PageID);

        return webPage;
        //get page access for user

        //get page controls 

        //get security table

        //check security 

        //translate 

        //check page access 

        //check user access 

        //if user has no access throw error

        //if user has access return object
    }
}
