using Sloth.Designer.Models;
using System.Collections.ObjectModel;

namespace Sloth.Designer.Services;
public interface IDesignerService
{
    Task<ObservableCollection<ListWebPageItem>?> ListWebPageByID(string? appID, string? pageID);
}