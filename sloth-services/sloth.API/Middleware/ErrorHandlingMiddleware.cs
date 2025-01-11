
using sloth.API.Extensions;
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
        catch (UserAlreadyExistsException ex)
        {
            await HandleExceptionAsync(context, ex, 409, "User already exists.");
        }
        catch (InvalidUserException ex)
        {
            await HandleExceptionAsync(context, ex, 400, "Invalid user.");
        }
        catch (InvalidTokenException ex)
        {
            await HandleExceptionAsync(context, ex, 400, "Invalid token.");
        }
        catch (JobCreationException ex)
        {
            await HandleExceptionAsync(context, ex, 500, "Error on creating new job request");
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
            type = statusCode.GetStatusTypeLink(),
            title = "Request exception occured.",
            status = statusCode,
            errors = new Dictionary<string, string[]>
            {
                { "Request", new[] { ex.Message } } // Correctly initialize the dictionary
            }
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(errorResponse);
    }
}
