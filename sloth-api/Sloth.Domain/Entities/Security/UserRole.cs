using Microsoft.AspNetCore.Identity;

namespace Sloth.Domain.Entities;

/// <summary>
/// This class extends IdentityRole, provided by Identity package
/// </summary>
public class UserRole : IdentityRole
{
    public string DisplayName { get; set; } = default!;
    public string? Description { get; set; }
}
