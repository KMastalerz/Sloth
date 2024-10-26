
namespace Sloth.Designer.Services;

public interface IAuthService
{
    Task<bool> Login(string username, string password);
    Task Refreshtoken(string username, string password);
    void Logoff();
}