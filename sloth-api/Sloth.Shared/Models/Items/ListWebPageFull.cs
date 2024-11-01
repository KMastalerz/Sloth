namespace Sloth.Shared.Models.Items;
public class ListWebPageFull
{
    public IEnumerable<WebPageFull> WebPages { get; set; } = [];
    public IEnumerable<WebPanelFull> WebPanels { get; set; } = [];
    public IEnumerable<WebSectionFull> WebSections { get; set; } = [];
    public IEnumerable<WebControlFull> WebControls { get; set; } = [];
}
