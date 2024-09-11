using Microsoft.AspNetCore.Identity;

namespace Sloth.Infrastructure.Entities;
/// <summary>
/// This class extends IdentityUser, provided by Identity package
/// User Claims will be used for additionally securing the controls
/// </summary>
public class User : IdentityUser
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? UserImageUrl { get; set; }
}