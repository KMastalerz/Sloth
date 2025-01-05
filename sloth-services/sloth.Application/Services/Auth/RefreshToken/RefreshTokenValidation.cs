using FluentValidation;

namespace sloth.Application.Services.Auth;
public class RefreshTokenValidation : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenValidation()
    {
        RuleFor(command => command.UserID)
            .NotEmpty()
            .WithMessage("UserID is required.");
        RuleFor(command => command.RefreshToken)
            .NotEmpty()
            .WithMessage("RefreshToken is required.");
    }
}
