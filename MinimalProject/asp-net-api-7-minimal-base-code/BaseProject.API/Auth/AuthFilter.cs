using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using BaseProject.Shared;
using BaseProject.Shared.ResponseObject;
using BaseProject.Core.Token;
using Amazon.Auth.AccessControlPolicy;
using BaseProject.Core.Service;

namespace BaseProject.API.Auth
{
    public class AuthFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var isAllowAnonymous = context.HttpContext.GetEndpoint()!.Metadata.Any(x => x.GetType() == typeof(AllowAnonymousAttribute));
            // would skip when endpoint allows anonymous
            if (isAllowAnonymous)
            {
                return await next(context);
            }
            else
            {
                var tokenService = context.HttpContext.RequestServices.GetService<TokenService>();
                var accountService = context.HttpContext.RequestServices.GetService<IAccountService>();
                var token = context.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                var isExpired = TokenService.CheckTokenExpired(token);
                if (isExpired)
                {
                    var principle = tokenService.GetPrincipalFromExpiredToken(token);
                    if (principle == null)
                    {
                        return new E<object>()
                        {
                            Code = StatusCode.BAD_REQUEST,
                            ErrorName = ErrorName.FORBIDDEN,
                            Message = ResponseMessage.OUT_OF_APP_WAY
                        };
                    }
                    else
                    {
                        var email = principle.Identity!.Name;
                        if (email == null)
                        {
                            return new E<object>()
                            {
                                Code = StatusCode.BAD_REQUEST,
                                ErrorName = ErrorName.FORBIDDEN,
                                Message = ResponseMessage.OUT_OF_APP_WAY
                            };
                        }
                        else
                        {
                            var currentUser = accountService.GetByEmail(email);
                            if(currentUser is null)
                                return new E<object>()
                                {
                                    Code = StatusCode.BAD_REQUEST,
                                    ErrorName = ErrorName.FORBIDDEN,
                                    Message = ResponseMessage.OUT_OF_APP_WAY
                                };
                            if (currentUser.RefreshTokenExpiryTime <= DateTime.Now)
                                return new E<object>()
                                {
                                    Code = StatusCode.UNAUTHOR,
                                    ErrorName = ErrorName.INVALID_REFRESH_TOKEN,
                                    Message = ResponseMessage.REQUIRED_LOGIN
                                };
                            else
                            {
                                return new E<object>()
                                {
                                    Code = StatusCode.UNAUTHOR,
                                    ErrorName = ErrorName.INVALID_REFRESH_TOKEN,
                                    Message = ResponseMessage.REQUIRED_REFRESH_TOKEN
                                };
                            }
                        }
                    }
                }
                return await next(context);
            }
        }
    }
}
