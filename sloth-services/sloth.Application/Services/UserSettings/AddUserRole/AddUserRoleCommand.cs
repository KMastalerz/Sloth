using MediatR;

namespace sloth.Application.Services.UserSettings;
public class AddUserRoleCommand : IRequest
{
    public Guid UserID { get; set; }
    public string RoleCode { get; set; } = default!;
}
