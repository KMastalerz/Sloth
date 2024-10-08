namespace Sloth.Application.DTO;
public class GetWebPage
{
    public string PageID { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Panels { get; set; } = default!;
    public string? Orientation { get; set; } = null;
    public string? Background { get; set; } = null;
    public string? Position { get; set; } = null;
    public string? Class { get; set; } = null;
    public string? Style { get; set; } = null;
    public string? MetaData { get; set; }
    public List<GetWebPanel> WebPanels { get; set; } = [];
}
