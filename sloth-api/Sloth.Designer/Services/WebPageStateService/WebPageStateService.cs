using Sloth.Designer.Models;

namespace Sloth.Designer.Services;

internal class WebPageStateService : IWebPageStateService
{
    public IEnumerable<ListWebPageItem> WebPages { get; set; } = [];
    public ListWebPageItem? WebPage { get; set; } = null;
}
