namespace Sloth.Domain.Entities;
/// <summary>
/// This tables holds all teams in Sloth
/// </summary>
public class Team
{
    public int TeamID { get; set; }
    public string TeamName { get; set; } = default!;
    public string? TeamImageURL { get; set; }
    public string TeamDescription { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string? Region { get; set; }
    public string City { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string BuildingNumber { get; set; } = default!;
    public string? BuildingSection { get; set; }
    public string? InternalNumber { get; set; }
    public string? ClientDescription { get; set; }

}
