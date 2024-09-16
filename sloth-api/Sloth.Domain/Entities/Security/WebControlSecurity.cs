namespace Sloth.Domain.Entities;
public class WebControlSecurity
{
    /// <summary>
    /// references WebPage
    /// </summary>
    public string PageID { get; set; } = default!;
    public string ControlID { get; set; } = default!;
    /// <summary>
    /// references UserGroup
    /// </summary>
    public string UserGroupID { get; set; } = default!;
    public bool? IsHidden { get; set; } = null;
    public bool? IsReadOnly { get; set; } = null;
    public bool? IsDisabled { get; set; } = null;
}
