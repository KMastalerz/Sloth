namespace Sloth.Domain.Entities;
/// <summary>
/// This table represents all releases of the product and its versioning details. 
/// MajorVersion: Indicates significant changes, often with backward-incompatible updates.
/// Minor Version: Represents backward-compatible improvements or additions.
/// Patch Version: Used for backward-compatible bug fixes.
/// Build Number: Often used to indicate specific builds or iterations, usually for internal tracking or continuous integration purposes.
/// </summary>
public class ProductReleases
{
    public Guid ReleaseID { get; set; }
    /// <summary>
    /// references Product
    /// </summary>
    public Guid ProductID { get; set; }
    public string ProductVersion { get; set; } = default!;
    public int? MajorVersion { get; set; }
    public int? MinorVersion { get; set; }
    public int? PatchVersion { get; set; }
    public int? BuildVersion { get; set; }
    public DateOnly? ReleaseDate { get; set; }
    public DateOnly? SupportEnd {  get; set; }
}
