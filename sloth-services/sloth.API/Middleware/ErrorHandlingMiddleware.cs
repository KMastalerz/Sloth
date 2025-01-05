
using sloth.Domain.Exceptions;

namespace sloth.API.Middleware;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (InvalidLoginException ex)
        {
            await HandleExceptionAsync(context, ex, 401, "Invalid login attempt.");
        }
        catch (InvalidUserRoleException ex)
        {
            await HandleExceptionAsync(context, ex, 403, "User does not have the required role.");
        }
        catch (LockedPasswordException ex)
        {
            await HandleExceptionAsync(context, ex, 423, "Password is locked.");
        }
        catch (LockedUserException ex)
        {
            await HandleExceptionAsync(context, ex, 423, "User account is locked.");
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, 500, "An unexpected error occurred.");
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex, int statusCode, string message)
    {
        logger.LogError(message);

        var errorResponse = new
        {
            ErrorCode = statusCode,
            ErrorMessage = message
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(errorResponse);
    }
}
