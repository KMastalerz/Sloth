namespace Sloth.Application.DTO;
public class GetWebSection
{
    public string PanelID { get; set; } = default!;
    public string SectionID { get; set; } = default!;
    public string SectionType { get; set; } = default!;
    public string? SectionLabel { get; set; } = null;
    public List<GetWebControl> WebControls { get; set; } = [];
}
