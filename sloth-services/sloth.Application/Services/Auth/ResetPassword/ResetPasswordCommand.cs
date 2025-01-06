using MediatR;

namespace sloth.Application.Services.Auth;
public class ResetPasswordCommand : IRequest
{
    public string Login { get; set; } = default!;
    public string SecurityCode { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
}
