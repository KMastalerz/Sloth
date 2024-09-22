using FluentValidation;

namespace Sloth.Application.Services.Security;
public class LoginCommandValidation : AbstractValidator<LoginCommand>
{
    public LoginCommandValidation()
    {
        RuleFor(user => user.Login)
            .NotEmpty()
            .WithMessage("Login has to be provided.");
        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("Password has to be provided.");
    }
}
