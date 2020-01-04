using System;
using Microsoft.AspNetCore.Builder;

namespace Betting_Aggregator.Utils
{
    public static class CorrelationMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<CorrelationMiddleware>();
        }
    }
}
