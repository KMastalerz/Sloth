namespace Sloth.Domain.Entities;
public class WebSection
{
    public string AppID { get; set; } = default!;
    public string PageID { get; set; } = default!;
    public string PanelID { get; set; } = default!;
    public string SectionID { get; set; } = default!;
    public string? Controls { get; set; } = null;
    public string? Position { get; set; } = null;
    public string? Label { get; set; } = null;
    public string? MetaData { get; set; } = null;
    public DateTime ChangeDate { get; set; } = DateTime.UtcNow!;
    public Guid ChangeUser { get; set; } = default!;
    public List<WebControl> WebControls { get; set; } = [];
}
