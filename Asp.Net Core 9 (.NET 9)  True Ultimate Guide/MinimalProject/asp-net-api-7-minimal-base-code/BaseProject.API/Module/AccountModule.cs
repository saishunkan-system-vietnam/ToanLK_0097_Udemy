using BaseProject.API.Exceptions;
using BaseProject.API.Filters;
using BaseProject.API.ServiceHelper;
using BaseProject.Core.Service;
using BaseProject.Core.Token;
using BaseProject.Shared;
using BaseProject.Shared.Entity;
using BaseProject.Shared.Modal.Dto;
using BaseProject.Shared.ResponseObject;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Agreement.JPake;
using System.Security.Claims;

namespace BaseProject.API.Module
{
    public class AccountModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            return builder;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            var account = endpoints.MapGroup("/account")
                            .WithTags("Accounts");

            account.MapGet("/verify-account/{verifyToken}", VerifyAccount).AllowAnonymous();
            account.MapPost("/login", Login).AllowAnonymous().AddEndpointFilter<ExceptionFilter>();
            account.MapPost("/sign-up", SignUp).AllowAnonymous();
            account.MapPost("/refresh-token", RefreshToken).AllowAnonymous();
            account.MapGet("/get-me", GetMe);
            account.MapPut("/", Update);
            account.MapPut("/password", ChangePassword);

            return endpoints;
        }

        public Response SignUp(Account newAccount, [FromServices] IAccountService accountService)
        {
            Account? addedAccount = accountService.SignUp(newAccount);

            if (addedAccount != null)
            {
                addedAccount.Password = "";
                return new R<Account>()
                {
                    Code = StatusCode.OK,
                    Message = ResponseMessage.REGISTER_SUCCESS,
                    Payload = addedAccount,
                };
            }
            else
                return new E<object>()
                {
                    Code = StatusCode.CONFLICT,
                    Message = ResponseMessage.ALLREADY_REGISTED
                };
        }

        public R<bool> ChangePassword(ChangePasswordDto newPassword, [FromServices] IAccountService accountService, HttpContext httmlContext)
        {
            var currentUser = accountService.GetMe(httmlContext);
            if (currentUser == null)
            {
                return new R<bool>()
                {
                    Payload = false
                };
            }

            if (!currentUser.Password.VerifyPassword(newPassword.CurrentPassword))
            {
                return new R<bool>()
                {
                    Payload = false
                };
            }

            currentUser.Password = newPassword.Password.HashPassword();
            accountService.Update(currentUser);
            return new R<bool>()
            {
                Payload = true
            };
        }

        public Response VerifyAccount(string verifyToken, [FromServices] TokenService tokenService, [FromServices] IAccountService accountService)
        {
            var principal = tokenService.GetPrincipalFromExpiredToken(verifyToken);
            if (principal == null)
                return new E<object>()
                {
                    Code = StatusCode.BAD_REQUEST,
                    ErrorName = ErrorName.FORBIDDEN,
                    Message = ResponseMessage.OUT_OF_APP_WAY
                };

            var email = principal.Identity!.Name; //this is mapped to the Name claim by default
            var user = accountService.GetByEmail(email!);
            if (user is null)
                return new E<object>()
                {
                    Code = StatusCode.BAD_REQUEST,
                    ErrorName = ErrorName.FORBIDDEN,
                    Message = ResponseMessage.OUT_OF_APP_WAY
                };

            bool isTokenExpired = TokenService.CheckTokenExpired(verifyToken);
            if (isTokenExpired)
            {
                return new E<object>()
                {
                    Code = StatusCode.BAD_REQUEST,
                    ErrorName = ErrorName.FORBIDDEN,
                    Message = ResponseMessage.EXPIRED_TOKEN_VERIFY
                };
            }
            else
            {
                user.EmailValidated = true;
                bool isSuccess = accountService.Update(user);
                return isSuccess ? new R<object>()
                {
                    Code = StatusCode.OK,
                    Message = ResponseMessage.ACCOUNT_VERIFY_SUCCESS,
                } : new E<object>()
                {
                    Code = StatusCode.INTERVAL_ERROR,
                    Message = ResponseMessage.SERVER_ERROR,
                };
            }
        }

        public Response Login(Account loginAccount, [FromServices] IAccountService accountService, [FromServices] TokenService tokenService)
        {
            var account = accountService.Login(loginAccount.Email, loginAccount.Password);
            if (account is null)
            {
                return new E<object>()
                {
                    Code = StatusCode.BAD_REQUEST,
                    Message = ResponseMessage.WRONG_ACCOUNT,
                    ErrorName = ErrorName.UNAUTHOR
                };
            }

            if (account.EmailValidated != true)
            {
                throw new HasntVerifyEmail("Verify your mail first");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Email),
                new Claim(ClaimTypes.Role, account.Role)
            };
            var token = tokenService.GenerateToken(claims, 1);
            var refreshToken = tokenService.GenerateRefreshToken();

            account.RefreshToken = refreshToken;
            account.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(2);
            accountService.Update(account);
            return new R<TokenDto>()
            {
                Code = StatusCode.OK,
                Message = ResponseMessage.LOGIN_SUCCESS,
                Payload = new TokenDto()
                {
                    Token = token,
                    RefreshToken = refreshToken
                }
            };
        }

        public Response RefreshToken(TokenDto token, [FromServices] TokenService tokenService, [FromServices] IAccountService accountService)
        {
            if (token is null)
                return new E<object>()
                {
                    Code = StatusCode.BAD_REQUEST,
                    ErrorName = ErrorName.INVALID_TOKEN,
                    Message = ResponseMessage.INVALID_TOKEN
                };
            string accessToken = token.Token;
            string refreshToken = token.RefreshToken;
            var principal = tokenService.GetPrincipalFromExpiredToken(accessToken);

            var email = principal.Identity!.Name; //this is mapped to the Name claim by default

            var user = accountService.GetByEmail(email!);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return new E<object>()
                {
                    Code = StatusCode.BAD_REQUEST,
                    ErrorName = ErrorName.INVALID_REFRESH_TOKEN,
                    Message = ResponseMessage.INVALID_REFRESH_TOKEN
                };
            var newAccessToken = tokenService.GenerateToken(principal.Claims, 1);
            var newRefreshToken = tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            accountService.Update(user);

            return new R<TokenDto>()
            {
                Code = StatusCode.OK,
                Payload = new TokenDto()
                {
                    Token = newAccessToken,
                    RefreshToken = newRefreshToken
                }
            };
        }

        public R<Account?> GetMe(HttpContext httpContext, TokenService tokenService, [FromServices] IAccountService accountService)
        {
            var currentUser = accountService.GetMe(httpContext);
            if (currentUser != null)
            {
                currentUser.Password = "";
            }
            return new R<Account?>()
            {
                Payload = currentUser
            };
        }

        public R<bool?> Update(Account modifiedUser, [FromServices] IAccountService accountService)
        {
            var getCurrentUser = accountService.GetByWhere(x => x.Email == modifiedUser.Email).FirstOrDefault();
            if(getCurrentUser == null)
            {
                return new R<bool?>()
                {
                    Payload = false
                };
            }
            modifiedUser.Password = getCurrentUser.Password;
            var result = accountService.Update(modifiedUser);
            return new R<bool?>()
            {
                Payload = result
            };
        }

    }
}
