namespace sloth.Application.UserIdentity;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}