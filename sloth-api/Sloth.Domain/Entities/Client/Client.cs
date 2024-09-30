namespace Sloth.Domain.Entities;
/// <summary>
/// This table holds all clients added to database
/// </summary>
public class Client
{
    public Guid ClientID { get; set; }
    public string ClientName { get; set; } = default!;
    public string? ClientImageURL { get; set; }
    public string? ClientDescription { get; set; }
    public string Country { get; set; } = default!;
    public string? Region { get; set; }
    public string City { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string BuildingNumber { get; set; } = default!;
    public string? BuildingSection { get; set; }
    public string? InternalNumber { get; set; }
}
