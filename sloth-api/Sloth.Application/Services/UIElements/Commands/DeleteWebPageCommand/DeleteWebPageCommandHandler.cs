using MediatR;
using Microsoft.Extensions.Logging;
using Sloth.Application.UserIdentity;
using Sloth.Domain.Entities;
using Sloth.Domain.Exceptions;
using Sloth.Domain.Repositories;

namespace Sloth.Application.Services.UIElements;
public class DeleteWebPageCommandHandler(ILogger<DeleteWebPageCommandHandler> logger, IUIElementsRepository uIElementsRepository, IUserContext userContext) : IRequestHandler<DeleteWebPageCommand>
{
    public async Task Handle(DeleteWebPageCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser()!;
        logger.LogInformation("User: {UserID} attempts to remove page: {PageID} for application: {AppID}", user.UserID, request.PageID, request.AppID);

        var deleteWebPage = await uIElementsRepository.GetWebPageAsync(request.AppID, request.PageID) ??
            throw new NotFoundException(nameof(WebPage), request.PageID);

        await uIElementsRepository.RemoveWebPageAsync(deleteWebPage);

        logger.LogInformation("Page: {PageID} for application: {AppID} was successfully removed!", request.PageID, request.AppID);
    }
}
