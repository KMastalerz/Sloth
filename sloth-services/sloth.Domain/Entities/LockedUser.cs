namespace sloth.Domain.Entities;
public class LockedUser
{
    public Guid UserID { get; set; } = default!;
    public DateTime ExpirationDate { get; set; }
}
