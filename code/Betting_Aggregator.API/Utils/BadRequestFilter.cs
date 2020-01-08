using System.Threading.Tasks;
using Betting_Aggregator.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Betting_Aggregator.API.Utils
{
    public class BadRequestFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                var handler = new ExceptionHandler();

                Task responseObject;
                if (context.Exception is BusinessException)
                {
                    responseObject = handler.HandleBusinessExceptionAsync(context.HttpContext, context.Exception as BusinessException);
                    context.Result = new ObjectResult(responseObject)
                    {
                        StatusCode = 400,
                    };
                }
                else
                {
                    responseObject = handler.HandleUnhandledExceptionAsync(context.HttpContext, context.Exception);
                    context.Result = new ObjectResult(responseObject)
                    {
                        StatusCode = 500,
                    };
                }
                context.ExceptionHandled = true;
            }
        }
    }
}
