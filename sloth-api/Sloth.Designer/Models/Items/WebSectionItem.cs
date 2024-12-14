using System.Collections.ObjectModel;

namespace Sloth.Designer.Models;

public class WebSectionItem
{
    public string AppID { get; set; } = default!;
    public string PageID { get; set; } = default!;
    public string PanelID { get; set; } = default!;
    public string SectionID { get; set; } = default!;
    public string? Controls { get; set; } = null;
    public string? Position { get; set; } = null;
    public string? Label { get; set; } = null;
    public string? Metadata { get; set; } = null;
    public ObservableCollection<WebControlItem> WebControls { get; set; } = [];
    public DateTime ChangeDate { get; set; } = DateTime.UtcNow!;
    public Guid ChangeUser { get; set; } = default!;
}
