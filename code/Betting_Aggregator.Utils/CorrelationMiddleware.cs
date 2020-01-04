using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Betting_Aggregator.Utils
{
    public class CorrelationMiddleware
    {
        public const string X_CORRELATION_ID = "x-correlation-id";
        private readonly RequestDelegate _next;

        public CorrelationMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(X_CORRELATION_ID, out var correlationId))
            {
                context.TraceIdentifier = correlationId;
            }
            else
            {
                correlationId = Guid.NewGuid().ToString();
                context.Request.Headers.Add(X_CORRELATION_ID, correlationId); 
                context.TraceIdentifier = correlationId;
            }

            context.Response.OnStarting(ctx =>
            {
                var httpContext = (HttpContext)ctx;
                httpContext.Response.Headers.Add(X_CORRELATION_ID, context.TraceIdentifier);
                return Task.CompletedTask;
            }, context);

            await _next(context);
        }
    }
}
