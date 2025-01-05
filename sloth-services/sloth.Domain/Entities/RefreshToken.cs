namespace sloth.Domain.Entities;
public class RefreshToken
{
    public Guid UserID { get; set; } = default;
    public string Token { get; set; } = default!;
    public DateTime ExpirationDate { get; set; } = default!;
}
