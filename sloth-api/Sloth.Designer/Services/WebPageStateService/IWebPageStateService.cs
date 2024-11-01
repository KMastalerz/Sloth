using Sloth.Designer.Core;
using Sloth.Designer.Enums;
using Sloth.Designer.Models;
using Sloth.Shared.Models;

namespace Sloth.Designer.Services;

public interface IWebPageStateService: IBaseStateService
{
    IEnumerable<string> WebApplications { get; set; }
    IEnumerable<ListWebPageItem> WebPages { get;  set;}
    WebPageItem? WebPage { get; set; }
    WebPanelItem? WebPanel { get; set; }
    WebSectionItem? WebSection { get; set; }
    WebControlItem? WebControl { get; set; }
    AddElementType? AddElementType { get; set; }
    void AddPanel(NewPanel panel);
    void AddSection(NewSection section);
    void AddControl(NewControl control);
    void DeletePanel(WebPanelItem panel);
    void DeleteSection(WebSectionItem panel);
    void DeleteControl(WebControlItem panel);
    Task RefreshWebApplications();
}