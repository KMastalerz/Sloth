using Sloth.Domain.Exceptions;

namespace Sloth.API.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(context, ex, 404, ex.Message);
        }
        catch (InvalidLoginException ex)
        {
            await HandleExceptionAsync(context, ex, 403, ex.Message);
        }
        catch (MissingSystemOptionException ex)
        {
            await HandleExceptionAsync(context, ex, 403, ex.Message);
        }
        catch (MissingAccessException ex)
        {
            await HandleExceptionAsync(context, ex, 401, ex.Message);
        }
        catch (InvalidPropertyException ex)
        {
            await HandleExceptionAsync(context, ex, 401, ex.Message);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, 500, "Something went wrong.");
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex, int statusCode, string message)
    {
        logger.LogError(ex, message);
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new { error = message });
    }
}
