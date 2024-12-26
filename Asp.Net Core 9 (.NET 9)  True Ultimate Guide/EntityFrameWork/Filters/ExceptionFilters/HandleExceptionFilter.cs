using Microsoft.AspNetCore.Mvc.Filters;

namespace EntityFrameWork.Filters.ExceptionFilters
{
    public class HandleExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine("Error occured");
        }
    }
}
