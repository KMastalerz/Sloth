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
    AddElementType? AddElementType { get; set; }
    void AddPanel(NewElementItem param);
    void AddSection(NewElementItem param);
    void AddPanelControl(NewElementItem param);
    void AddSectionControl(NewElementItem param);
    void DeletePanel(WebPanelItem panel);
    void DeleteSection(WebSectionItem panel);
    void DeletePanelControl(WebControlItem panel);
    void DeleteSectionControl(WebControlItem panel);
}