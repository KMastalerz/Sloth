namespace Sloth.Shared.DTO;

public class GetWebPanel
{
    public string PageID { get; set; } = default!;
    public string PanelID { get; set; } = default!;
    public string PanelType { get; set; } = default!;
    public string? Sections { get; set; } = null;
    public string? Class { get; set; } = null;
    public string? Label { get; set; } = null;
    public string? Metadata { get; set; } = null;
    public List<GetWebSection> WebSections { get; set; } = [];
}
