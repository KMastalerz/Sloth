using MediatR;
using sloth.Application.Models.Auth;

namespace sloth.Application.Services.Auth;
public class LoginCommand : IRequest<AccessTokenResponse>
{
    public string Login { get; set; } = default!;
    public string Password { get; set; } = default!;
}
