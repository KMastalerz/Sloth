namespace Sloth.Infrastructure.DTO.Config;
public class IdentityApiEndpointRouteBuilderOptions
{
    public bool ExcludeRegisterPost { get; } = true;
    public bool ExcludeLoginPost { get; } = false;
    public bool ExcludeRefreshPost { get; } = false;
    public bool ExcludeConfirmEmailGet { get; } = true;
    public bool ExcludeResendConfirmationEmailPost { get; } = true;
    public bool ExcludeForgotPasswordPost { get; } = false;
    public bool ExcludeResetPasswordPost { get; } = false;
    public bool ExcludeManageGroup { get; } = true;
    public bool Exclude2faPost { get; } = true;
    public bool ExcludegInfoGet { get; } = true;
    public bool ExcludeInfoPost { get; } = true;
}
