using MediatR;
using Sloth.Application.DTO.Security;

namespace Sloth.Application.Services.Security;
public class RefreshTokenCommand : IRequest<AccessTokenResponse>
{   
    public string UserName { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}
