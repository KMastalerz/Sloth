using MediatR;

namespace sloth.Application.Services.UserSettings;
public class UpdateInfoCommand : IRequest
{
    public Guid UserID { get; set; }
    public string UserName { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
}
