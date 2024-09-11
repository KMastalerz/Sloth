namespace Sloth.Domain.Entities;
/// <summary>
/// This table holds all services in Sloth
/// </summary>
public class Service
{
    public int ServiceID { get; set; } 
    public string ServiceName { get; set; } = default!;
    public string? ServiceImageUrl { get; set; }
    public string ServiceDescription { get; set; } = default!;
}
