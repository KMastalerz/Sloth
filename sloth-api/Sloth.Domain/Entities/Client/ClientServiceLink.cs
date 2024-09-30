namespace Sloth.Domain.Entities;
/// <summary>
/// This table links clients to services provided to them.
/// </summary>
public class ClientServiceLink
{
    /// <summary>
    /// references Client
    /// </summary>
    public Guid ClientID { get; set; }
    /// <summary>
    /// references Service
    /// </summary>
    public Guid ServiceID { get; set; }
}
