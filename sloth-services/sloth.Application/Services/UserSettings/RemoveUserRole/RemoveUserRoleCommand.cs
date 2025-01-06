using MediatR;

namespace sloth.Application.Services.UserSettings;
public class RemoveUserRoleCommand : IRequest
{
    public Guid UserID { get; set; }
    public string RoleCode { get; set; } = default!;
}
