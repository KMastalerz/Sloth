using MediatR;

namespace sloth.Application.Services.UserSettings;
public class UpdatePasswordCommand : IRequest
{
    public Guid UserID { get; set; }
    public string OldPassword { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
}
