using Microsoft.AspNetCore.Identity;

namespace Sloth.Infrastructure.Entities;
/// <summary>
/// This class extends IdentityRole, provided by Identity package
/// </summary>
public class UserRole : IdentityRole
{
    public string? Description { get; set; }
}
