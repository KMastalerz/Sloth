namespace Sloth.Domain.Entities;
/// <summary>
/// This tables linkes clients and product that are bought and/or used by them. 
/// </summary>
public class ClientProductLink
{
    /// <summary>
    /// references Client
    /// </summary>
    public Guid ClientID { get; set; }
    /// <summary>
    /// references Product
    /// </summary>
    public Guid ProductID { get; set; }
}
