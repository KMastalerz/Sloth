using Sloth.Designer.Models;

namespace Sloth.Designer.Services;

public interface IAuthService
{
    Task<ServiceReturnValue<AccessTokenItem>?> Login(string username, string password);
    Task<ServiceReturnValue<AccessTokenItem>?> Refreshtoken(string username, string password);
}