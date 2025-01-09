namespace sloth.Domain.Entities;
public class UserTeamLink
{
    public Guid UserID { get; set; } // FK
    public Guid TeamID { get; set; } // FK
    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; } = null;
}
