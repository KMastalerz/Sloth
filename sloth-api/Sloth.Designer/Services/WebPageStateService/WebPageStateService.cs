using Sloth.Designer.Core;
using Sloth.Designer.Enums;
using Sloth.Designer.Models;
using Sloth.Shared.Models;
using Sloth.Shared.Helpers;


namespace Sloth.Designer.Services;

internal class WebPageStateService : BaseStateService, IWebPageStateService
{
    private readonly IDesignerService designerService;
    public WebPageStateService(IDesignerService designerService)
    {
        this.designerService = designerService;

        Task.Run(async () =>
        {
            await TaskRefreshWebApplications();
        });
    }

    // Public properties for state
    private IEnumerable<string> webApplications { get; set; } = [];
    public IEnumerable<string> WebApplications
    {
        get => webApplications;
        set
        {
            webApplications = value;
            NotifyStateChanged(value); // Notify when WebApplications changes
        }
    }
    private IEnumerable<ListWebPageItem> webPages { get; set; } = [];
    public IEnumerable<ListWebPageItem> WebPages
    {
        get => webPages;
        set
        {
            webPages = value;
            NotifyStateChanged(value); // Notify when WebPages changes
        }
    }
    private WebPageItem? webPage { get; set; } = null;
    public WebPageItem? WebPage
    {
        get => webPage;
        set
        {
            webPage = value;
            NotifyStateChanged(value); // Notify when WebPage changes
        }
    }
    private WebPanelItem? webPanel { get; set; }
    public WebPanelItem? WebPanel
    {
        get => webPanel;
        set
        {
            webPanel = value;
            NotifyStateChanged(value); // Notify when WebPanel changes
        }
    }
    private WebSectionItem? webSection { get; set; }
    public WebSectionItem? WebSection
    {
        get => webSection;
        set
        {
            webSection = value;
            NotifyStateChanged(value); // Notify when WebSection changes
        }
    }
    private WebControlItem? webControl { get; set; }
    public WebControlItem? WebControl
    {
        get => webControl;
        set
        {
            webControl = value;
            NotifyStateChanged(value);
        }
    }
    private AddElementType? addElementType { get; set; }
    public AddElementType? AddElementType
    {
        get => addElementType;
        set
        {
            addElementType = value;
            NotifyStateChanged(value); // Notify when AddElementType changes
        }
    }

    public void AddPanel(NewPanel panel)
    {
        if(WebPage == null) return;

        var webPanel = new WebPanelItem()
        {
            AppID = WebPage.AppID,
            PageID = WebPage.PageID,
            PanelID = panel.PanelID.ToCamelCase(),
            PanelType = panel.PanelType,
        };

        if(panel.Sections is not null)
            foreach(var section in panel.Sections)
            {
                webPanel.WebSections.Add(new()
                {
                    AppID = webPanel!.AppID,
                    PageID = webPanel!.PageID,
                    PanelID = webPanel!.PanelID,
                    SectionID = section.ToCamelCase()
                });
            }

        WebPage.WebPanels.Add(webPanel);
    }
    public void AddSection(NewSection section)
    {
        if (WebPanel == null) return;
        WebPanel?.WebSections.Add(new()
        {
            AppID = WebPanel!.AppID,
            PageID = WebPanel!.PageID,
            PanelID = WebPanel!.PanelID,
            SectionID = section.SectionID.ToCamelCase()
        });
    }
    public void AddPanelControl(NewControl control)
    {
        if (WebPanel == null) return;
        WebPanel.WebControls.Add(new()
        {
            AppID = WebPanel.AppID,
            PageID = WebPanel.PageID,
            PanelID = WebPanel.PanelID,
            SectionID = null,
            ControlID = control.ControlID.ToCamelCase(),
            ControlType = control.ControlType
        });
    }
    public void AddSectionControl(NewControl control)
    {
        if (WebSection == null) return;
        WebSection.WebControls.Add(new()
        {
            AppID = WebSection.AppID,
            PageID = WebSection.PageID,
            PanelID = WebSection.PanelID,
            SectionID = WebSection.SectionID,
            ControlID = control.ControlID.ToCamelCase(),
            ControlType = control.ControlType
        });
    }
    public void DeletePanel(WebPanelItem panel)
    {
        if (WebPage == null) return;
        WebPage.WebPanels.Remove(panel);
    }
    public void DeleteSection(WebSectionItem section)
    {
        if (WebPage == null) return;
        var webSections = WebPage.WebPanels.FirstOrDefault(p => p.PanelID == section.PanelID)?.WebSections;
        webSections?.Remove(section);
    }
    public void DeletePanelControl(WebControlItem control)
    {
        if (WebPage == null) return;
        var webControls = WebPage.WebPanels.FirstOrDefault(p => p.PanelID == control.PanelID)?.WebControls;
        webControls?.Remove(control);
    }
    public void DeleteSectionControl(WebControlItem control)
    {
        if (WebPage == null) return;
        var webControls = WebPage.WebPanels.FirstOrDefault(p => p.PanelID == control.PanelID)?
            .WebSections.FirstOrDefault(s => s.SectionID == control.SectionID)?.WebControls;
        webControls?.Remove(control);
    }

    public async Task TaskRefreshWebApplications()
    {
        var response = await designerService.ListWebApplicationIDs();

        if (response?.Success == true)
        {
            WebApplications = response.Data!;
        }
        else
        {
            WebApplications = [];
        }
        NotifyStateChanged(WebApplications);
    }
}
