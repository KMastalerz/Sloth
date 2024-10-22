using Sloth.Designer.Core;
using Sloth.Shared.Models;

namespace Sloth.Designer.Services;

public interface IWebPageStateService: IBaseStateService
{
    IEnumerable<string> WebApplications { get; set; }
    IEnumerable<ListWebPageItem> WebPages { get;  set;}
    ListWebPageItem? WebPage { get; set; }
}