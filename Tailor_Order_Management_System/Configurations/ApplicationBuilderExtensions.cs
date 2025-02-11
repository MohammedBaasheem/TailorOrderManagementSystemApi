using Microsoft.AspNetCore.Diagnostics;

namespace Tailor_Order_Management_System.Configurations
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder app)=> app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
         
    }

}
