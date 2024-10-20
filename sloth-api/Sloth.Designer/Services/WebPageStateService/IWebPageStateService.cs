using Sloth.Designer.Models;

namespace Sloth.Designer.Services;

public interface IWebPageStateService
{
    IEnumerable<ListWebPageItem> WebPages { get;  set;}
    ListWebPageItem? WebPage { get; set; }
}