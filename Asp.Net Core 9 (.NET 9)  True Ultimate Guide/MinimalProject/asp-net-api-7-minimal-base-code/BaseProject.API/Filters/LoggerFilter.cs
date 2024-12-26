using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

public class LoggerFilter : IEndpointFilter
{
    private readonly ILogger<LoggerFilter> _logger;

    public LoggerFilter(ILogger<LoggerFilter> logger)
    {
        _logger = logger;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var httpContext = context.HttpContext;
        var request = httpContext.Request;

        _logger.LogInformation($"Incoming request: {request.Method} {request.Path}");
        _logger.LogInformation($"Executing endpoint: {httpContext.GetEndpoint()?.DisplayName}");

        foreach (var argument in context.Arguments)
        {
            _logger.LogInformation($"Action argument: {argument}");
        }

        object? result;
        try
        {
            result = await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Endpoint {httpContext.GetEndpoint()?.DisplayName} threw an exception: {ex.Message}");
            throw;
        }

        _logger.LogInformation($"Successfully executed endpoint: {httpContext.GetEndpoint()?.DisplayName}");
        _logger.LogInformation($"Response status code: {httpContext.Response.StatusCode}");

        return result;
    }
}
