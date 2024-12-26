using Microsoft.AspNetCore.Mvc.Filters;

namespace EntityFrameWork.Filters.ResultFilters
{
    public class GetPeopleResultFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {

            context.HttpContext.Response.Headers["Last-Modified"] = DateTime.Now.ToString();
            await next.Invoke();
        } 
    }
}
