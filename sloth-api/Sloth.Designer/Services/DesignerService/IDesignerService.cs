using Sloth.Shared.Models;
using System.Collections.ObjectModel;

namespace Sloth.Designer.Services;
public interface IDesignerService
{
    Task<ObservableCollection<ListWebPageItem>?> ListWebPageByID(string? appID, string? pageID);
    Task<IEnumerable<string>?> ListWebApplicationIDs();
    Task<WebPageItem?> GetFullWebPage(string appID, string pageID);
    Task<string?> SaveFullWebPage(WebPageItem webPage);
}