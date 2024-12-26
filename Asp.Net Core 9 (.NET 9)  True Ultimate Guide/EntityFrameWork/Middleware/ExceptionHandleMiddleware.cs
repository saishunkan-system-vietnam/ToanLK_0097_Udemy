using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EntityFrameWork.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
               await _next(httpContext);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                await httpContext.Response.WriteAsync("Error");
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandleMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandleMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandleMiddleware>();
        }
    }
}
