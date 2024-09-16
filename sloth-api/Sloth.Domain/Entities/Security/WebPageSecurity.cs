namespace Sloth.Domain.Entities;
public class WebPageSecurity
{
    /// <summary>
    /// references WebPage
    /// </summary>
    public string PageID { get; set; } = default!;
    /// <summary>
    /// references UserGroup
    /// </summary>
    public string UserGroupID { get; set; } = default!;
    public bool CanAccess { get; set; } = true;
    public bool CanAdd { get; set; } = true;
    public bool CanDelete { get; set; } = true;
}
