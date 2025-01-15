using MediatR;

namespace sloth.Application.Services.Auth;
public class HealthCheckCommand(string login) : IRequest<bool>
{
    public string Login { get; set; } = login;
}
