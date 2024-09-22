namespace Sloth.Domain.Entities;
public class FailedAttempt
{
    public Guid UserID { get; set; } = default;
    public int Count { get; set; }
}
