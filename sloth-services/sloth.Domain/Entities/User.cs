namespace sloth.Domain.Entities;
public class User
{
    public Guid UserID { get; set; } = new Guid();
    public string UserName { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string LanguageCode { get; set; } = "en"; //ISO 639-1
    public int FailedLoginAttempts { get; set; }
    public bool IsActive { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = [];
    public ICollection<Team> Teams { get; set; } = [];
}
