namespace Sloth.Domain.Entities;
/// <summary>
/// This table links team with services which team is providing
/// </summary>
public class TeamServiceLink
{
    /// <summary>
    /// references Team
    /// </summary>
    public int TeamID { get; set; }
    /// <summary>
    /// references Service
    /// </summary>
    public int ServiceID { get; set; }
}
