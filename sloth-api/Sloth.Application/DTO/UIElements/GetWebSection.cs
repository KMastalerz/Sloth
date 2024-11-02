namespace Sloth.Application.DTO;

public class GetWebSection
{
    public string PageID { get; set; } = default!;
    public string PanelID { get; set; } = default!;
    public string SectionID { get; set; } = default!;
    public string? Controls { get; set; } = null;
    public string? Position { get; set; } = null;
    public string? Label { get; set; } = null;
    public string? Metadata { get; set; } = null;
    public List<GetWebControl> WebControls { get; set; } = [];
}
