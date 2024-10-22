using Sloth.Designer.Core;
using Sloth.Shared.Models;

namespace Sloth.Designer.Services;

internal class WebPageStateService : BaseStateService, IWebPageStateService
{
    private readonly IDesignerService designerService;
    public WebPageStateService(IDesignerService designerService)
    {
        this.designerService = designerService;

        Task.Run(async () =>
        {
            WebApplications = await designerService.ListWebApplicationIDs() ?? [];
            NotifyStateChanged(WebApplications);
        });
    }

    // Public properties for state
    private IEnumerable<string> _webApplications = [];
    public IEnumerable<string> WebApplications
    {
        get => _webApplications;
        set
        {
            _webApplications = value;
            NotifyStateChanged(value); // Notify when WebApplications changes
        }
    }

    private IEnumerable<ListWebPageItem> _webPages = [];
    public IEnumerable<ListWebPageItem> WebPages
    {
        get => _webPages;
        set
        {
            _webPages = value;
            NotifyStateChanged(value); // Notify when WebPages changes
        }
    }
    private ListWebPageItem? webPage { get; set; } = null;
    public ListWebPageItem? WebPage
    {
        get => webPage;
        set
        {
            webPage = value;
            NotifyStateChanged(value); // Notify when WebPage changes
        }
    }
}
