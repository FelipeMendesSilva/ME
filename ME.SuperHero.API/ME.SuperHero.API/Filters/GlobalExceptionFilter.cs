using ME.SuperHero.Domain.Result;
using Microsoft.AspNetCore.Diagnostics;

namespace ME.SuperHero.API.Filters
{    
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
       {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var result = Result.Failure("Server Error", System.Net.HttpStatusCode.InternalServerError);
            await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);

            return true;
        }
    }
}
