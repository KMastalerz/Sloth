namespace Sloth.Application.DTO;
public class GetWebPage
{
    public string PageID { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Panels { get; set; } = default!;
    public string? MetaData { get; set; }
    public List<GetWebPanel> WebPanels { get; set; } = [];
}
