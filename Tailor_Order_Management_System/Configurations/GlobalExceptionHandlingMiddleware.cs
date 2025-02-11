using Newtonsoft.Json;
using System.Net;
using Tailor_Order_Management_System.Exceptions;
using NotImplementedException = Tailor_Order_Management_System.Exceptions.NotImplementedException;
using UnauthorizedAccessException = Tailor_Order_Management_System.Exceptions.UnauthorizedAccessException;

namespace Tailor_Order_Management_System.Configurations
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch (exception)
            {
                case UnauthorizedAccessException unauthorizedAccessException:
                    code = HttpStatusCode.Unauthorized;
                    result = JsonConvert.SerializeObject(new { Error = exception.Message });
                    break;
                case NotImplementedException notImplementedException:
                    code = HttpStatusCode.NotImplemented;
                    result = JsonConvert.SerializeObject(new { Error = exception.Message });
                    break;
                case NotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    result = JsonConvert.SerializeObject(new { Error = exception.Message });
                    break;
                case System.Collections.Generic.KeyNotFoundException keyNotFoundException:
                    code = HttpStatusCode.NotFound;
                    result = JsonConvert.SerializeObject(new { Error = exception.Message });
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    result = JsonConvert.SerializeObject(new { Error = exception.Message });
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
