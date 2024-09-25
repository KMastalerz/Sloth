namespace Sloth.Application.DTO;
public class GetWebPanel
{
    public string PanelID { get; set; } = default!;
    public string? PanelLabel { get; set; } = null;
    public string PanelType { get; set; } = default!;
    public List<string>? Sections { get; set; } = null;
    public List<GetWebSection> WebSections { get; set; } = [];
}
