namespace Sloth.Domain.Entities;
/// <summary>
/// Used to store security information for a web page.
/// </summary>
public class WebPageSecurity
{
    public string AppID { get; set; } = default!;
    public string PageID { get; set; } = default!;
    public string UserGroup { get; set; } = default!;
    public bool CanAccess { get; set; } = true;
    public bool CanAdd { get; set; } = true;
    public bool CanDelete { get; set; } = true;
    public DateTime ChangeDate { get; set; } = default!;
    public Guid ChangeUser { get; set; } = default!;
}
