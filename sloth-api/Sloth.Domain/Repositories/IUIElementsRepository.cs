using Sloth.Domain.Entities;

namespace Sloth.Domain.Repositories;

public interface IUIElementsRepository
{
    Task<WebPage?> GetWebPageAsync(string pageID);
    Task<IEnumerable<WebControl>?> ListWebControlAsync(string pageID);
    Task<IEnumerable<WebPanel>?> ListWebPanelAsync(string pageID);
}