namespace Sloth.Designer.Models;
public class ListWebPagesItem
{
    public IEnumerable<WebPageItem> WebPages { get; set; } = [];
    public IEnumerable<WebPanelItem> WebPanels { get; set; } = [];
    public IEnumerable<WebSectionItem> WebSections { get; set; } = [];
    public IEnumerable<WebControlItem> WebControls { get; set; } = [];
}
