using MediatR;
using Sloth.Domain.DTO;

namespace Sloth.Application.Services.Security;
public class RefreshTokenCommand : IRequest<AccessTokenResponse>
{   
    public string UserName { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}
