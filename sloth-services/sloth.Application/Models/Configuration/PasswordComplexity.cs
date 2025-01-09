namespace sloth.Application.Models.Configuration;
public class PasswordComplexity
{
    public int RequiredLength { get; set; } = 16;
    public int RequiredUniqueChars { get; set; } = 8;
    public bool RequireNonAlphanumeric { get; set; } = true;
    public bool RequireLowercase { get; set; } = true;
    public bool RequireUppercase { get; set; } = true;
    public bool RequireDigit { get; set; } = true;
}
