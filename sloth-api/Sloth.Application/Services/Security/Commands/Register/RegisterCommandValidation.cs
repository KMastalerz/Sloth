using FluentValidation;
using Sloth.Domain.Repositories;

namespace Sloth.Application.Services.Security;
public class RegisterCommandValidation : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidation(ISecurityRepository securityRepository)
    {
        // TO DO: Add validation based on configuration and system options like Min, Regex, Last Used etc...
        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("Password cannot be an empty string.");

        RuleFor(user => user.UserName)
            .NotEmpty()
            .WithMessage("Username cannot be an empty string.")
            .Custom((value, context) =>
            {
                var checkUsername = securityRepository.ValidateUserNameAsync(value).Result;
                if (checkUsername)
                {
                    context.AddFailure($"Username: {value} is already taken.");
                }
            }); ;

        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("Email cannot be an empty string.")
            .EmailAddress()
            .WithMessage("Email is not in the correct format.")
            .Custom((value, context)=>
            {
                var checkEmail = securityRepository.ValidateEmailAsync(value).Result;
                if (checkEmail)
                {
                    context.AddFailure($"Email: {value} is already taken.");
                }
            });

        RuleFor(user => user.FirstName)
            .NotEmpty()
            .WithMessage("First name cannot be an empty string.");

        RuleFor(user => user.LastName)
            .NotEmpty()
            .WithMessage("Last name cannot be an empty string.");

        RuleFor(user => user.ConfirmPassword)
            .Equal(user => user.Password)
            .WithMessage("Field confirm password has to match password.");
    }
}
