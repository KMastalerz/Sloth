using MediatR;
using Sloth.Shared.DTO;

namespace Sloth.Application.Services.Security;
public class RefreshTokenCommand : IRequest<AccessTokenResponse>
{   
    public string UserName { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}
