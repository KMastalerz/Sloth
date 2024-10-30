using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Sloth.Application.UserIdentity;
using Sloth.Domain.Constants;
using Sloth.Domain.Entities;
using Sloth.Domain.Repositories;

namespace Sloth.Application.Services.UIElements;
public class SaveFullWebPageCommandHandler(ILogger<SaveFullWebPageCommandHandler> logger, IUIElementsRepository uIElementsRepository, IMapper mapper, IUserContext userContext) : IRequestHandler<SaveFullWebPageCommand, string?>
{
    public async Task<string?> Handle(SaveFullWebPageCommand request, CancellationToken cancellationToken)
    {
        //var user = userContext.GetCurrentUser();
        //logger.LogInformation("User: {UserID} attempts to save changes for page {PageID}", user.UserID, request.WebPage.PageID);

        try
        {
            var webPage = mapper.Map<WebPage>(request.WebPage);

            await uIElementsRepository.SaveFullWebPageAsync(webPage);
        }
        catch
        {
            logger.LogError("Error while saving changes for page {PageID}", request.WebPage.PageID);
            return SaveMessages.SaveFailed;
        }

        return SaveMessages.SaveSuccess;
    }
}
