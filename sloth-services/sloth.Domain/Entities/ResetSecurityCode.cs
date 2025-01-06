namespace sloth.Domain.Entities;
public class ResetSecurityCode
{
    public Guid UserID { get; set; }
    public string SecurityCode { get; set; } = default!;
    public DateTime ExpirationDate { get; set; }
}
