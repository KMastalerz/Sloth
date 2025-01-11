namespace sloth.Domain.Entities;
public class Client
{
    public Guid ClientID { get; set; }
    public string Name { get; set; } = default!;
    public string Alias { get; set;} = default!;
    public string Country { get; set; } = default!;
    public string City { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string? BuildingNumber { get; set; } = null;
    public string? ApartmentNumber { get; set; } = null;
    public bool IsActive { get; set; }


    /// <summary>
    /// External properties
    /// </summary>

    public List<Product> Products { get; set; } = [];
}
