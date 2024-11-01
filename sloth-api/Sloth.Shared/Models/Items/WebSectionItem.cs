using System.Collections.ObjectModel;

namespace Sloth.Shared.Models;

public class WebSectionItem
{
    public string AppID { get; set; } = default!;
    public string PageID { get; set; } = default!;
    public string PanelID { get; set; } = default!;
    public string SectionID { get; set; } = default!;
    public string? Controls { get; set; } = null;
    public string? Position { get; set; } = null;
    public string? Label { get; set; } = null;
    public string? MetaData { get; set; } = null;
    public ObservableCollection<WebControlItem> WebControls { get; set; } = [];
}
