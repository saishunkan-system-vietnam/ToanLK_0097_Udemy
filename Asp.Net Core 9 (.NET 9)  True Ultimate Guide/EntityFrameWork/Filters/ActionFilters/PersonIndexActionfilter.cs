using EntityFrameWork.Controller;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EntityFrameWork.Filters.ActionFilters
{
    public class PersonIndexActionfilter : IActionFilter
    {
        private readonly ILogger<PersonIndexActionfilter> _logger;
        public PersonIndexActionfilter(ILogger<PersonIndexActionfilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("{filterName}.{MethodName} method",nameof(PersonIndexActionfilter),nameof(OnActionExecuted));
            PersonController controller = (PersonController)context.Controller;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.ContainsKey("guid"))
            {
                string guidParam = context.ActionArguments["guid"].ToString();
                Guid GuidResult = Guid.Empty;
                bool isParseSucceed = Guid.TryParse(guidParam, out GuidResult);
                if (isParseSucceed)
                {
                    Console.WriteLine("Done");
                }
                else
                {
                    context.ActionArguments["guid"] = Guid.NewGuid().ToString();
                }
            }
        }
    }
}
