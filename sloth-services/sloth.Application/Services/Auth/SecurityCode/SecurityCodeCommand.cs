using MediatR;

namespace sloth.Application.Services.Auth;
public class SecurityCodeCommand : IRequest
{
    public string Login { get; set; } = default!;
}
