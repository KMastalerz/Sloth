using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Sloth.Application.UserIdentity;
using Sloth.Domain.Entities;
using Sloth.Domain.Repositories;
using System.Transactions;

namespace Sloth.Application.Services.UIElements;
public class SaveFullWebPageCommandHandler(ILogger<SaveFullWebPageCommandHandler> logger, IUIElementsRepository uIElementsRepository, IMapper mapper, IUserContext userContext) : IRequestHandler<SaveFullWebPageCommand>
{
    public async Task Handle(SaveFullWebPageCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser()!;
        logger.LogInformation("{UserID} attempts to save changes for page: {PageID}", user.UserID, request.WebPage.PageID);

        using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TransactionManager.MaximumTimeout
        }, TransactionScopeAsyncFlowOption.Enabled))
        {
            try
            {
                var checkWebPage = await uIElementsRepository.CheckWebPageExistsAsync(request.WebPage.AppID, request.WebPage.PageID);

                var webPage = mapper.Map<WebPage>(request.WebPage);

                if (checkWebPage)
                {
                    await uIElementsRepository.SaveWebPageAsync(webPage);
                }
                else await uIElementsRepository.AddWebPageAsync(webPage);

                transaction.Complete();
                logger.LogInformation("Page: {PageID} was successfully changed!", request.WebPage.PageID);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An error occurred while saving changes for page {PageID}", request.WebPage.PageID);
                throw; 
            }
        }

    }
}
