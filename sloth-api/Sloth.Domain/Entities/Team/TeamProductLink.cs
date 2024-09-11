namespace Sloth.Domain.Entities;
/// <summary>
/// This table links team and product which team is maintaining
/// </summary>
public class TeamProductLink
{
    /// <summary>
    /// references Team
    /// </summary>
    public int TeamID { get; set; }
    /// <summary>
    /// references Product
    /// </summary>
    public int ProductID { get; set; }
}
