using Sloth.Domain.Entities;

namespace Sloth.Domain.Repositories;

public interface IUIElementsRepository
{
    Task<WebPage?> GetWebPageAsync(string PageID, string UserRole);
    Task<WebPage?> GetLoginWebPageAsync(string PageID);
    Task<IEnumerable<WebControl>?> ListWebControlAsync(string PageID);
    Task<IEnumerable<SecurityTable>?> ListControlSecurityAsync(string PageID, string UserRole, string? PageSecurityTableID);
}