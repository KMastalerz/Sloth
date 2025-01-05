namespace sloth.Domain.Entities;
public class LockedUser
{
    public Guid UserID { get; set; } = default!;
    public DateTime LockExpirationDate { get; set; }
}
