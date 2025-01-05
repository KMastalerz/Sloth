using FluentValidation;
using Microsoft.Extensions.Configuration;
using sloth.Application.Models.Configuration;
using sloth.Domain.Cache;
using sloth.Utilities.Constants;

namespace sloth.Application.Services.Auth;
public class RegisterCommandValidaton : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidaton(
        IConfiguration configuration,
        IRoleCache roleCache)
    {
        var appConfig = configuration.GetSection(ConfigurationKeys.Configuration).Get<Configuration>()!;
        var passwordConfig = appConfig.PasswordComplexity;

        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("Username is required");

        RuleFor(x => x.Email)
            .NotEmpty().
            WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email is not valid");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(passwordConfig.RequiredLength)
            .WithMessage($"Password must be at least {passwordConfig.RequiredLength} characters long")
            .Matches(@"[0-9]").When(_ => passwordConfig.RequireDigit)
            .WithMessage("Password must contain a digit")
            .Matches(@"[a-z]").When(_ => passwordConfig.RequireLowercase)
            .WithMessage("Password must contain a lowercase letter")
            .Matches(@"[A-Z]").When(_ => passwordConfig.RequireUppercase)
            .WithMessage("Password must contain an uppercase letter")
            .Matches(@"\W").When(_ => passwordConfig.RequireNonAlphanumeric)
            .WithMessage("Password must contain a non-alphanumeric character")
            .Must(password => password.Distinct().Count() >= passwordConfig.RequiredUniqueChars)
            .WithMessage($"Password must contain at least {passwordConfig.RequiredUniqueChars} unique characters");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage("Passwords do not match");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required");

        RuleFor(x => x.RoleCode)
            .NotEmpty()
            .WithMessage("Role is required")
            .Must(roleCode => roleCache.RoleExists(roleCode ?? ""))
            .WithMessage("The specified role does not exist.");
    }
}
