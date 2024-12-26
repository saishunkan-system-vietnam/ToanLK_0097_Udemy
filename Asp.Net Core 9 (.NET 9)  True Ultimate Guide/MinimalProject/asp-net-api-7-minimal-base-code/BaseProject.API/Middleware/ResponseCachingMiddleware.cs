using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.IO;
using System.Threading.Tasks;
using static GrapeCity.Enterprise.Data.DataEngine.ExpressionEvaluation.Eval;

public class ResponseCachingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration;
    private readonly ILogger<ResponseCachingMiddleware> _logger;

    public ResponseCachingMiddleware(RequestDelegate next, IMemoryCache cache, ILogger<ResponseCachingMiddleware> logger,TimeSpan cacheDuration)
    {
        _next = next;
        _cache = cache;
        _cacheDuration = cacheDuration;
        _logger = logger;

    }

    public async Task InvokeAsync(HttpContext context)
    {
        
        if (IsRunningMiddleware(context))
        {
            var cacheKey = $"{context.Request.Path}{context.Request.QueryString}";
            _logger.LogDebug($"cacheKey: {cacheKey}");
            if (_cache.TryGetValue(cacheKey, out string cachedResponse))
            {
                _logger.LogInformation($"Request {context.Request.Path} is already cached");
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(cachedResponse);
                return;
            }
            _logger.LogInformation($"Request {context.Request.Path} starting to cache response");
            var originalBodyStream = context.Response.Body;
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await _next(context);

            memoryStream.Seek(0, SeekOrigin.Begin);
            var responseBody = new StreamReader(memoryStream).ReadToEnd();
            memoryStream.Seek(0, SeekOrigin.Begin);

            _cache.Set(cacheKey, responseBody, _cacheDuration);

            await memoryStream.CopyToAsync(originalBodyStream);
            context.Response.Body = originalBodyStream;
        }
        else
        {
            await _next(context);
        }

    }
    public bool IsRunningMiddleware(HttpContext context)
    {
        return (context.Request.Path.ToString() == "/api/product/cache");
    }
}
