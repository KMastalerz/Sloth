namespace Sloth.Domain.Entities;
public class CookieKey
{
    public Guid UserID { get; set; } = default!;
    public string? Key { get; set; }
    public DateTime? ExpirationDate { get; set; }
}
