namespace Sloth.Application.DTO;

public class GetWebPanel
{
    public string PageID { get; set; } = default!;
    public string PanelID { get; set; } = default!;
    public string PanelType { get; set; } = default!;
    public string? Sections { get; set; } = null;
    public string Controls { get; set; } = default!;
    public string? Class { get; set; } = null;
    public string? Style { get; set; } = null;
    public string? Title { get; set; } = null;
    public string? MetaData { get; set; } = null;
    public List<GetWebControl> WebControls { get; set; } = [];
}
