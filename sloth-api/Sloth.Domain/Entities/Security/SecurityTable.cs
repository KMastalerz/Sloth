namespace Sloth.Domain.Entities;
/// <summary>
/// Table provides security on control level per user group. 
/// </summary>
public class SecurityTable
{
    public string SecurityTableID { get; set; } = default!;
    public string ControlID { get; set; } = default!;
    public string UserGroup { get; set; } = default!;
    public bool IsHidden { get; set; } = false; 
    public bool IsReadOnly { get; set; } = false;
    public bool IsDisabled { get; set; } = false;
    public bool IsRequired { get; set; } = false;
}
