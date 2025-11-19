using Microsoft.AspNetCore.Diagnostics;

namespace Api.ExceptionHandler;

public sealed class DefaultExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var error = new { success = false, message = exception.Message };
        await httpContext.Response.WriteAsJsonAsync(error, cancellationToken);
        return true;
    }
}