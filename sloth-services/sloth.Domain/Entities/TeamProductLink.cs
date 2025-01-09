namespace sloth.Domain.Entities;
public class TeamProductLink
{
    public Guid TeamID { get; set; } // FK
    public int ProductID { get; set; } // FK
}
