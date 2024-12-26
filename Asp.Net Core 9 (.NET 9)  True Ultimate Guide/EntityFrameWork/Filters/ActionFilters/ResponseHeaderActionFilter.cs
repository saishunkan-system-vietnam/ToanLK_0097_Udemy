using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EntityFrameWork.Filters.ActionFilters
{
    public class ResponseHeaderActionFilter : IAsyncActionFilter, IOrderedFilter
    {
        private readonly ILogger<ResponseHeaderActionFilter> _logger;
        private readonly string _headerKey;
        private readonly string _headerValue;
        public int Order { get; }

        public ResponseHeaderActionFilter(ILogger<ResponseHeaderActionFilter> logger, string headerKey, string headerValue, int order)
        {
            _logger = logger;
            _headerKey = headerKey;
            _headerValue = headerValue;
            Order = order;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            await next.Invoke();
            context.HttpContext.Response.Headers[this._headerKey] = _headerValue;
        }
    }
}
