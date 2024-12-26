using Amazon.S3;
using Amazon.S3.Model;
using BaseProject.API.Auth;
using BaseProject.API.ServiceHelper;
using BaseProject.Core.Mail;
using BaseProject.Core.Service;
using BaseProject.Shared.Entity;
using BaseProject.Shared.Modal.Dto;
using BaseProject.Shared.Modal.Entity;
using BaseProject.Shared.Modal.Enum;
using BaseProject.Shared.ResponseObject;
using GrapeCity.Enterprise.Data.Expressions.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Agreement.JPake;
using System.Drawing;
using System.Text.RegularExpressions;

namespace BaseProject.API.Module
{
    public class NewsModule : IModule
    {

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            return builder;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            var news = endpoints.MapGroup("/news")
                        .WithTags("type");

            news.MapGet("/", GetAllNews);
            news.MapPost("/", AddType);
            news.MapDelete("/{guid}", DeleteType);
            news.MapPost("/delete",DeleteRange);
            news.MapPut("/", Update);
            news.MapGet("/{id}", GetById);
            news.MapGet("/newest/{mount}", GetNewestPost);

            return endpoints;
        }
        public R<List<Shared.Modal.Entity.News>> GetAllNews([FromServices] INewsService newsService)
        {
            return new R<List<Shared.Modal.Entity.News>>()
            {
                Payload = newsService.Get().ToList()
            };
        }

        public R<List<Shared.Modal.Entity.News>> GetNewestPost(int mount,[FromServices] INewsService newsService)
        {
            var result = newsService.Get().OrderByDescending((item) => item.Content).Take(mount).ToList();
            return new R<List<Shared.Modal.Entity.News>>()
            {
                Payload = result
            };
        }

        public R<Shared.Modal.Entity.News> GetById(Guid id,[FromServices] INewsService newsService)
        {
            var result = newsService.GetByWhere((news) => news.Id == id).FirstOrDefault();
            return new R<News> { 
                Payload = result
            };
        }

        public R<bool> AddType( NewsInsertDto newInsert, [FromServices] INewsService typeService, IAccountService accountService, HttpContext httpContext)
        {
            var currentAccount = accountService.GetMe(httpContext);
            if (currentAccount == null)
            {
                return new R<bool>() {
                    Payload = false
                };
            }
            newInsert.news.AuthorId = currentAccount.Id;
            var result = typeService.AddNews(newInsert);


            return new R<bool>()
            {
                Payload = result != null
            };
        }

        public R<bool> Update(NewsInsertDto modifiedNews,[FromServices] INewsService newsService)
        {
            newsService.UpdateNews(modifiedNews);
            return new R<bool>()
            {
                Payload = true
            };
        }

        public R<bool> DeleteType(Guid guid, [FromServices] INewsService newSerivce)
        {
            var result = newSerivce.Delete(t => t.Id == guid);
            return new R<bool>()
            {
                Payload = result
            };
        }

        public R<bool> DeleteRange(List<Guid> guids, [FromServices] INewsService newsService)
        {
            newsService.DeleteRange(guids);
            return new R<bool>
            {
                Payload = true
            };
        }

    }
}
