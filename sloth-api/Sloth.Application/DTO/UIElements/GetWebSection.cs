namespace Sloth.Application.DTO;

public class GetWebSection
{
    public string PageID { get; set; } = default!;
    public string PanelID { get; set; } = default!;
    public string SectionID { get; set; } = default!;
    public string Controls { get; set; } = default!;
    public string? Label { get; set; } = null;
    public string? MetaData { get; set; } = null;
    public List<GetWebControl> WebControls { get; set; } = [];
}
