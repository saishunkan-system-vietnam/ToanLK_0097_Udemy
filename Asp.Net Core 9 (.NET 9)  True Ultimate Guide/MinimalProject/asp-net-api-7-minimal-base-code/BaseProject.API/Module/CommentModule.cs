using BaseProject.API.ServiceHelper;
using BaseProject.Core.Service;
using BaseProject.Core.SignalR;
using BaseProject.Core.Token;
using BaseProject.Shared.Entity;
using BaseProject.Shared.Modal.Dto;
using BaseProject.Shared.ResponseObject;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BaseProject.API.Module
{
    public class CommentModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            return builder;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            var comment = endpoints.MapGroup("/comment-sender")
                            .WithTags("CommentSender");

            comment.MapGet("/",SendMessage).AllowAnonymous();

            return endpoints;
        }

        public R<object> SendMessage([FromServices] CommentHub hub)
        {
            var message = "this is a message from server";
            var user = "KhanhToanLe";
            hub.SendComment(user, user +" saying: "+ message);
            return new R<object>() { };
        }
    }
}
