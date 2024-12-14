namespace Sloth.Shared.DTO;
public class ListWebPagesItem
{
    public IEnumerable<GetWebPageFull> WebPages { get; set; } = [];
    public IEnumerable<GetWebPanelFull> WebPanels { get; set; } = [];
    public IEnumerable<GetWebSectionFull> WebSections { get; set; } = [];
    public IEnumerable<GetWebControlFull> WebControls { get; set; } = [];
}
