using Microsoft.AspNetCore.Diagnostics;

namespace V_Tube.Api.Middlewares
{
    public class GlobalExceptionMiddleware : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = 500;
            await httpContext.Response.WriteAsJsonAsync(new
            {
                message = exception.Message,
                statusCode = 500,
                isSuccess = false,
                result = default(Object)
            });
            return true;
        }
    }
}
