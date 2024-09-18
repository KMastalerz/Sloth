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
    public string ControlID { get; set; } = default!;
    /// <summary>
    /// references UserRole
    /// </summary>
    public string UserGroup { get; set; } = default!;
    public bool IsHidden { get; set; } = false; 
    public bool IsReadOnly { get; set; } = false;
    public bool IsDisabled { get; set; } = false;
}
