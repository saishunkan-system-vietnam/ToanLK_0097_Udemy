
using BaseProject.API.Exceptions;
using GrapeCity.Enterprise.Data.Expressions.Tools;
using BaseProject.Shared.ResponseObject;
namespace BaseProject.API.Filters
{
    public class ExceptionFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            object? result = null;
            try
            {
                result = await next.Invoke(context);
            }
            catch(Exception exception)
            {
                switch(exception)
                {
                    case HasntVerifyEmail:
                        await context.HttpContext.Response.WriteAsJsonAsync(new R<HasntVerifyEmail>()
                        {
                            Code = 500,
                            Message = exception.Message,
                            Timestamp = DateTime.Now
                        });
                        break;
                    case ArgumentException:
                        await context.HttpContext.Response.WriteAsJsonAsync(new R<ArgumentException>()
                        {
                            Code = 401,
                            Message = exception.Message,
                            Timestamp = DateTime.Now
                        });
                        break;
                    case NullReferenceException:
                        await context.HttpContext.Response.WriteAsJsonAsync(new R<NullReferenceException>()
                        {
                            Code = 400,
                            Message = exception.Message,
                            Timestamp = DateTime.Now
                        });
                        break;
                    case CannotUnloadAppDomainException:
                        await context.HttpContext.Response.WriteAsJsonAsync(new R<CannotUnloadAppDomainException>()
                        {
                            Code = 402,
                            Message = exception.Message,
                            Timestamp = DateTime.Now
                        });
                        break;
                }
            }
            return result;
        }
    }
}
