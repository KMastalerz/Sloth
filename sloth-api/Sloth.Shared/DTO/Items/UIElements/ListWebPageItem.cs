namespace Sloth.Shared.DTO;
public class ListWebPageItem
{
    public string AppID { get; set; } = default!;
    public string PageID { get; set; } = default!;
    public string Title { get; set; } = default!;
    public DateTime ChangeDate { get; set; } = default!;
    public string ChangeUser { get; set; } = default!;
}
