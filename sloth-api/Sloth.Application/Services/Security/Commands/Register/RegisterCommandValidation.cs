using FluentValidation;

namespace Sloth.Application.Services.Security;
public class RegisterCommandValidation : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidation()
    {
        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("Password cannot be an empty string.");


    }
}
