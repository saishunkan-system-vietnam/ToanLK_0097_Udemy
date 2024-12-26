using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System.Diagnostics;

namespace HttpSection
{
    public static class MainHttp
    {
        public static void Program(WebApplication app)
        {
            // INTRODUCTION
            app.MapGet("/introduction", () =>
            {
                return "Hello World";
            });

            // STATUS CODE
            app.MapGet("/statusCode", (HttpContext context) =>
            {
                context.Response.StatusCode = 404;
                context.Response.ContentType = "text/html";
                context.Response.WriteAsync("Hello World");
            });

            // RESPONSE HEADER
            app.MapGet("/responseHeader", async (HttpContext context) =>
            {
                var responseHeader = context.Response.Headers;
                context.Response.StatusCode = 302;
                responseHeader.Date = DateTime.Now.ToString();
                responseHeader.Server = "Ketsel";
                responseHeader.ContentType = "text/html";
                responseHeader.ContentLength = 1000L;
                responseHeader.CacheControl = "max-age=5000";
                responseHeader.SetCookie = "user_id=12";
                responseHeader.AccessControlAllowOrigin = "http://localhost:8181";
                // redirect if status code is [Redirect Message] (3xx)
                responseHeader.Location = "http://localhost:8155/introduction";
                await context.Response.WriteAsync(@"<h1 style=""color:red"">Hello World</h1>");
            });

            // HTTP REQUEST
            app.MapGet("/httpRequest", (HttpContext context) =>
            {
                var path = context.Request.Path;
                var method = context.Request.Method;
                context.Response.WriteAsync($"{method}-{path}");
            });

            // HTTP QUERY STRING 
            // https://localhost:8155/queryString?id=12&value=helloworld
            app.MapGet("/queryString", (HttpContext context) =>
            {
                var id = context.Request.Query["id"];
                var value = context.Request.Query["value"];
                context.Response.WriteAsync($"{id}-{value}");
            });

            // REQUEST HEADER
            app.MapGet("/requestHeader", (HttpContext context) =>
            {
                var accept = context.Request.Headers.Accept;
                var acceptLang = context.Request.Headers.AcceptLanguage;
                var contentType = context.Request.Headers.ContentType;
                var contentLength = context.Request.Headers.ContentLength;
                var date = context.Request.Headers.Date;
                var host = context.Request.Headers.Host;
                var userAgent = context.Request.Headers.UserAgent;
                var cookie = context.Request.Headers.Cookie;
                return $"{accept} {acceptLang} {contentType} {contentLength} {date} {host} {userAgent} {cookie}";
            });

            // GET from POSTMAN
            app.MapGet("/postmanGet",async (HttpContext context) =>
            {
                await context.Response.WriteAsync("Hello POSTMAN");
            });

            app.MapPost("/postmanPost",async (HttpContext context) =>
            {
                StreamReader reader = new StreamReader(context.Request.Body);
                var body = await reader.ReadToEndAsync();
                Dictionary<string, StringValues> queryDic = QueryHelpers.ParseQuery(body);
                var nameVal = queryDic["name"][0];
                var numVal = queryDic["number"][0];
                Console.WriteLine(nameVal);
                Console.WriteLine(numVal);
            });
        }
    }
}
