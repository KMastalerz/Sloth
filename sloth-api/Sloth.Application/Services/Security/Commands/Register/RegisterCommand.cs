using MediatR;

namespace Sloth.Application.Services.Security;
public class RegisterCommand : IRequest
{
    public string? UserName { get; set; }
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? UserImageUrl { get; set; }
    public string UserGroupID { get; set; } = default!;
    public string LanguageCode { get; set; } = "ENU";
    public int? ThemeID { get; set; }
}
