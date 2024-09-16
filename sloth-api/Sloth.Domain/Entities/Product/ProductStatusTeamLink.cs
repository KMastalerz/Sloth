namespace Sloth.Domain.Entities;
public class ProductStatusTeamLink
{
    /// <summary>
    /// references Product
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// references JobStatus
    /// </summary>
    public int StatusID { get; set; }
    /// <summary>
    /// references Team
    /// </summary>
    public int TeamID { get; set; }

}
