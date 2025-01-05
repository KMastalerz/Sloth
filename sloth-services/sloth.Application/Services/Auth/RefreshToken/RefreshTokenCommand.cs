using MediatR;
using sloth.Application.Models.Auth;

namespace sloth.Application.Services.Auth;
public class RefreshTokenCommand : IRequest<AccessTokenResponse>
{
    public Guid UserID { get; set; }
    public string RefreshToken { get; set; } = default!;
}
