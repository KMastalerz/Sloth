using Sloth.Domain.Exceptions;

namespace Sloth.API.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException ex)
        {
            logger.LogWarning(ex, ex.Message);
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (InvalidLoginException ex)
        {
            logger.LogWarning(ex, ex.Message);
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (MissingSystemOptionException ex)
        {
            logger.LogWarning(ex, ex.Message);
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (MissingAccessException ex)
        {
            logger.LogWarning(ex, ex.Message);
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong.");
        }
    }
}
