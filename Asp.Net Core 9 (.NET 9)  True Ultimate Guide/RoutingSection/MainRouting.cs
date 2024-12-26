using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RoutingSection.CustomRouteConstraints;
using System.Runtime.InteropServices;

namespace RoutingSection
{
    public static class MainRouting
    {
        public static void Program(WebApplication app)
        {
            // find best match of end point
            app.UseRouting();

            app.Use(async (context, next) =>
            {
                var endpoint = context.GetEndpoint();
                if (endpoint != null)
                {
                    Console.WriteLine(endpoint!.DisplayName);
                    Console.WriteLine(endpoint.RequestDelegate!.Target);
                }
                await next.Invoke(context);
            });

            //execute the code related to the end point that be found in useRouting()
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("map1", async (HttpContext context) =>
                {
                    await context.Response.WriteAsync("Hello World from endpoint map1");
                });

                endpoints.MapPost("map2", async (HttpContext context) =>
                {
                    await context.Response.WriteAsync("Hello World from endpoint map2");
                });
            });

            #region ROUTE PARAMETER

            // use parameter to access route parameter value
            app.MapGet("/route/{name}.{extension}", (string name, string extension, HttpContext context) =>
            {
                context.Response.WriteAsync($"{name}.{extension}");
            });

            // use RouteValue to access route parameter value
            app.MapGet("/route/{name}-{age}", async (HttpContext context) =>
            {
                var routeValue = context.Request.RouteValues;
                await context.Response.WriteAsync($"{routeValue["name"]}-{routeValue["age"]}");
            });

            #endregion

            #region DEFAULT PARAMETER

            app.MapGet("default/{param=default-value}", async (string param, HttpContext context) =>
            {
                await context.Response.WriteAsync("Default Param: " + param);
            });

            #endregion

            #region OPTIONAL PARAMETER

            app.MapGet("optional/{id?}", async (HttpContext context) =>
            {
                var isContainIdKey = context.Request.RouteValues.ContainsKey("id");
                if (isContainIdKey)
                {
                    await context.Response.WriteAsync("optional value:" + context.Request.RouteValues["id"]);
                }
                else
                {
                    await context.Response.WriteAsync("optional ");
                }
            });

            #endregion

            #region ROUTE CONSTRAINTS

            // constraint int
            app.MapGet("constraints/int/{intValue:int?}", (HttpContext context) =>
            {
                var intValue = int.Parse(context.Request.RouteValues["intValue"].ToString());
                context.Response.WriteAsync(intValue.ToString());
            });

            //constraint boolean
            app.MapGet("constraints/boolean/{value:bool?}", (HttpContext context) =>
            {
                var value = context.Request.RouteValues["value"].ToString();
                context.Response.WriteAsync(value.ToString());
            });

            //constraint datetime
            //2024-01-01 11:11 AM
            app.MapGet("constraints/datetime/{value:datetime?}", (HttpContext context) =>
            {
                var value = context.Request.RouteValues["value"].ToString();
                context.Response.WriteAsync(value.ToString());
            });

            //constraints decimal
            app.MapGet("constraints/decimal/{value:decimal?}", (HttpContext context) =>
            {
                var value = context.Request.RouteValues["value"].ToString();
                context.Response.WriteAsync(value.ToString());
            });

            //constraints long
            app.MapGet("constraints/long/{value:long?}", (HttpContext context) =>
            {
                var value = context.Request.RouteValues["value"].ToString();
                context.Response.WriteAsync(value.ToString());
            });

            //constraints guid
            app.MapGet("constraints/guid/{value:guid?}", (HttpContext context) =>
            {
                var value = context.Request.RouteValues["value"].ToString();
                context.Response.WriteAsync(value.ToString());
            });

            //constraints min length
            app.MapGet("constraints/minlength/{value:minlength(3)?}", (HttpContext context) =>
            {
                var value = context.Request.RouteValues["value"].ToString();
                context.Response.WriteAsync(value.ToString());
            });

            //constraints max length
            app.MapGet("constraints/maxlength/{value:maxlength(5)?}", (HttpContext context) =>
            {
                var value = context.Request.RouteValues["value"].ToString();
                context.Response.WriteAsync(value.ToString());
            });

            //constraints length
            app.MapGet("constraints/length/{value:length(4)?}", (HttpContext context) =>
            {
                var value = context.Request.RouteValues["value"].ToString();
                context.Response.WriteAsync(value.ToString());
            });

            //constraints min
            app.MapGet("constraints/min/{value:min(4)?}", (HttpContext context) =>
            {
                var value = context.Request.RouteValues["value"].ToString();
                context.Response.WriteAsync(value.ToString());
            });

            //constraints max
            app.MapGet("constraints/max/{value:max(4)?}", (HttpContext context) =>
            {
                var value = context.Request.RouteValues["value"].ToString();
                context.Response.WriteAsync(value.ToString());
            });

            //constraints range
            app.MapGet("constraints/range/{value:range(3,5)?}", (HttpContext context) =>
            {
                var value = context.Request.RouteValues["value"].ToString();
                context.Response.WriteAsync(value.ToString());
            });

            //constraints alpha (only alphabet)
            app.MapGet("constraints/alpha/{value:alpha?}", (HttpContext context) =>
            {
                var value = context.Request.RouteValues["value"].ToString();
                context.Response.WriteAsync(value.ToString());
            });

            //constraints regex
            app.MapGet("constraints/regex/{value:regex(^\\d$)}", (HttpContext context) =>
            {
                var value = context.Request.RouteValues["value"].ToString();
                context.Response.WriteAsync(value.ToString());
            });

            #endregion

            #region CUSTOM ROUTE CONSTRAINT

            // month route constraint
            app.MapGet("constraints/month/{value:monthConstraint}", (HttpContext context) =>
            {
                var value = context.Request.RouteValues["value"].ToString();
                context.Response.WriteAsync(value.ToString());
            });

            #endregion

        }
    }
}
