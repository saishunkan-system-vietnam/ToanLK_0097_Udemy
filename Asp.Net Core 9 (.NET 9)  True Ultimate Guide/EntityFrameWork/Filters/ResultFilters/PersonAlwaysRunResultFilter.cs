using Microsoft.AspNetCore.Mvc.Filters;

namespace EntityFrameWork.Filters.ResultFilters
{
    public class PersonAlwaysRunResultFilter : IAlwaysRunResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Filters.OfType<SkipFilter>().Any())
            {
                return;
            }
            Console.WriteLine();
        }
    }
}
