
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
            await HandleExceptionAsync(context, ex, 401);
        }
        catch (InvalidUserRoleException ex)
        {
            await HandleExceptionAsync(context, ex, 403);
        }
        catch (LockedPasswordException ex)
        {
            await HandleExceptionAsync(context, ex, 423);
        }
        catch (LockedUserException ex)
        {
            await HandleExceptionAsync(context, ex, 423);
        }
        catch (UserAlreadyExistsException ex)
        {
            await HandleExceptionAsync(context, ex, 409);
        }
        catch (InvalidUserException ex)
        {
            await HandleExceptionAsync(context, ex, 400);
        }
        catch (InvalidTokenException ex)
        {
            await HandleExceptionAsync(context, ex, 400);
        }
        catch (JobCreationException ex)
        {
            await HandleExceptionAsync(context, ex, 500);
        }
        catch (InvalidJobTypeException ex)
        {
            await HandleExceptionAsync(context, ex, 400);
        }
        catch (MissingUserContextException ex)
        {
            await HandleExceptionAsync(context, ex, 401);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, 500);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex, int statusCode)
    {
        logger.LogError(ex.Message);

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
