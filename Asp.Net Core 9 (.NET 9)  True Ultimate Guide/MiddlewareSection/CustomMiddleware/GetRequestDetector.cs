using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSection.CustomMiddleware
{
    public class GetRequestDetector
    {
        private RequestDelegate _next;
        public GetRequestDetector(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            if(context.Request.Method == "GET")
            {
                Console.WriteLine("This is get data request");
            }

            await _next.Invoke(context);
            Console.WriteLine("Get data successfully");
        }
    }
    public static class GetRequestDetectorExtensions
    {
        public static IApplicationBuilder UseGetRequestDetector(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GetRequestDetector>();
        }
    }
}

