namespace Sloth.Domain.Entities;
public class SecurityTable
{
    /// <summary>
    /// Relates to actual DB Table to which security applies to
    /// </summary>
    public string SecurityTableID { get; set; } = default!;
    /// <summary>
    /// There has to exist such column in table
    /// </summary>
    public string ControlName { get; set; } = default!;
    /// <summary>
    /// references UserGroup
    /// </summary>
    public string UserGroupID { get; set; } = default!;
    public bool IsHidden { get; set; } = false; 
    public bool IsReadOnly { get; set; } = false;
    public bool IsDisabled { get; set; } = false;
}
