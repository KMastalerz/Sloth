using Microsoft.AspNetCore.Identity;

namespace Sloth.Domain.Entities;
/// <summary>
/// This class extends IdentityUser, provided by Identity package
/// User Claims will be used for additionally securing the controls
/// </summary>
public class User : IdentityUser
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? UserImageUrl { get; set;}
    /// <summary>
    /// this flag will define what language does use see in application
    /// </summary>
    public string? LanguageCode { get; set; } = "ENU";
    /// <summary>
    /// flag that will define what theme is used by user
    /// </summary>
    public int? ThemeID { get; set; }
}