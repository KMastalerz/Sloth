using MediatR;

namespace sloth.Application.Services.Auth;
public class RegisterCommand() : IRequest
{
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string RoleCode { get; set; } = default!;
}
