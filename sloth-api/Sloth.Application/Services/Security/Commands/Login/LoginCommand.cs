using MediatR;
using Sloth.Shared.DTO;

namespace Sloth.Application.Services.Security;
public class LoginCommand: IRequest<AccessTokenResponse>
{
    public string Login { get; set; } = default!;
    public string Password { get; set; } = default!;
}
