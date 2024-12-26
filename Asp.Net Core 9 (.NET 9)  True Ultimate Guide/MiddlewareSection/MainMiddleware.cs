using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MiddlewareSection.CustomMiddleware;

namespace MiddlewareSection
{
    public static class MainMiddleware
    {
        public static void Program(WebApplication app)
        {
            
            
            // USE 0
            app.Use(async (HttpContext context,RequestDelegate next) =>
            {
                await context.Response.WriteAsync("Hello World - Use\n");
                await next.Invoke(context);
                await context.Response.WriteAsync("Bai World - Use\n");
            });

            // USE 1
            app.Use(async (HttpContext context, RequestDelegate next) =>
            {
                await context.Response.WriteAsync("Hello World - Use - 1\n");
                await next.Invoke(context);
                await context.Response.WriteAsync("Bai World - Use - 1\n");
            });

            // CUSTOM MIDDLEWARE
            app.UseGetRequestDetector();

            // CONDITIONNAL MIDDLEWARE
            app.UseWhen(context => context.Request.Method == "GET",
                app => app.Use(async (HttpContext context,RequestDelegate next) =>
                {
                    await context.Response.WriteAsync("Hello from Use when\n");
                    await next.Invoke(context);
                })
            );

            // terminal middleware (always terminal)
            // (app.useStaticFiles is conditional terminal)
            app.Run(async (HttpContext context) =>
            {
                Console.WriteLine("Hello World");
            });


            // USE 2
            app.Use(async (HttpContext context, RequestDelegate next) =>
            {
                await context.Response.WriteAsync("Hello World - Use - 2\n");
                await next.Invoke(context);
                await context.Response.WriteAsync("Bai World - Use - 2\n");
            });
        }
    }
}
