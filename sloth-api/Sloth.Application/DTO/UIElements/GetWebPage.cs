namespace Sloth.Application.DTO;
public class GetWebPage
{
    public string PageID { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string? Class { get; set; } = null;
    public string? Style { get; set; } = null;
    public List<string>? Panels { get; set; } = null;
    public List<GetWebPanel> WebPanels { get; set; } = [];
}
