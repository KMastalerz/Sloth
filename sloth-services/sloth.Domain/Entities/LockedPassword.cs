namespace sloth.Domain.Entities;
public class LockedPassword
{
    public Guid UserID { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public DateTime ExpirationDate { get; set; }
}
