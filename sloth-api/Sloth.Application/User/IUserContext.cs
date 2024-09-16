namespace Sloth.Application.UserIdentity;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}