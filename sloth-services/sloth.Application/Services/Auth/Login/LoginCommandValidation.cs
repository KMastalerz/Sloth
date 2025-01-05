using FluentValidation;

namespace sloth.Application.Services.Auth;
public class LoginCommandValidation : AbstractValidator<LoginCommand>
{
    public LoginCommandValidation()
    {
        RuleFor(command => command.Login)
            .NotEmpty()
            .WithMessage("Login is required.");

        RuleFor(command => command.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}
