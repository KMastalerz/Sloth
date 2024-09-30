namespace Sloth.Domain.Entities;
/// <summary>
/// This table links parent-child teams together
/// </summary>
public class CrossTeamLink
{
    /// <summary>
    /// references Team
    /// </summary>
    public Guid ParentTeamID { get; set; }
    /// <summary>
    /// references Team
    /// </summary>
    public Guid ChildTeamID { get; set; }
}
