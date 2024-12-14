namespace Sloth.Shared.DTO;
public class GetWebPage
{
    public string PageID { get; set; } = default!;
    public string? Label { get; set; } = null;
    public string? Panels { get; set; } = null;
    public string? Background { get; set; } = null;
    public string? Class { get; set; } = null;
    public bool HasRouter { get; set; } = default!;
    public string? Layout { get; set; } = null;
    public string? Metadata { get; set; } = null;
    public List<GetWebPanel> WebPanels { get; set; } = [];
}
