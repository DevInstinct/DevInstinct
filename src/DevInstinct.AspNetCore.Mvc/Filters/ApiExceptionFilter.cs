using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace DevInstinct.AspNetCore.Mvc.Filters
{
    // https://weblog.west-wind.com/posts/2016/oct/16/error-handling-and-exceptionfilter-dependency-injection-for-aspnet-core-apis
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            ApplicationError apiError = null;
            if (context.Exception is ApplicationErrorException)
            {
                apiError = ((ApplicationErrorException)context.Exception)?.Error;
                context.HttpContext.Response.StatusCode = (context.Exception as ApiException)?.StatusCode ?? ApiException.DefaultStatusCode;
                context.Exception = null;
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                apiError = new ApplicationError("unauthorized", "Unauthorized Access");
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            else
            {
#if !DEBUG
                var msg = "An unexpected error occurred.";                
                string stack = null;
#else
                var msg = context.Exception.GetBaseException().Message;
                var stack = context.Exception.StackTrace;
#endif
                apiError = new ApplicationError("unexpected_error", msg)
                {
                    Detail = stack
                };
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            context.Result = new ObjectResult(apiError);

            base.OnException(context);
        }
    }
}
