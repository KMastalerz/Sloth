namespace sloth.Domain.Entities;
public class UserTeamLink
{
    public Guid UserID { get; set; }
    public Guid TeamID { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; } = null;
}
