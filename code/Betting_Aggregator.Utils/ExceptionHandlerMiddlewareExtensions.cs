using Microsoft.AspNetCore.Builder;

namespace Betting_Aggregator.Utils
{
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder;
            //return builder.UseMiddleware<ExceptionHandler.ExceptionHandler>();
        }
    }
}