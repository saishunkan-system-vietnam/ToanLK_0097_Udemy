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
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.Text.RegularExpressions;

namespace BaseProject.API.Module
{
    public class TypeModule : IModule
    {

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            return builder;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            var type = endpoints.MapGroup("/type")
                        .WithTags("type");
            type.MapGet("/", GetAllType);
            type.MapPost("/", AddType);
            type.MapDelete("/{guid}", DeleteType);
            type.MapPut("/", UpdateFlagShow);
            type.MapGet("/get-by-landing-page", GetByShowInLandingPage);
            type.MapPut("/update",Update);

            return endpoints;
        }
        public R<List<Shared.Modal.Entity.Type>> GetAllType([FromServices] ITypeService typeService)
        {
            var payload = typeService.Get().FirstOrDefault();
            foreach(var i in payload.Products){
                Console.WriteLine(i.Name);
            }


            return new R<List<Shared.Modal.Entity.Type>>()
            {
                Payload = new List<Shared.Modal.Entity.Type>(){ payload }
            };
        }

        public R<bool> Update(TypeInsertDto modifiedType,[FromServices] ITypeService typeService)
        {
            var result = typeService.Update(modifiedType);
            return new R<bool>()
            {
                Payload = result
            };
        }

        public R<bool> AddType(TypeInsertDto addedType, [FromServices] ITypeService typeService)
        {
            var result = typeService.AddType(addedType);

            return new R<bool>()
            {
                Payload = result
            };

        }

        public R<bool> DeleteType(Guid guid, [FromServices] ITypeService typeService)
        {
            var result = typeService.Delete(t => t.Id == guid);
            return new R<bool>()
            {
                Payload = result
            };

        }

        public R<bool> UpdateFlagShow(TypeUpdateFlagShowDto updateDto, [FromServices] ITypeService typeService)
        {
            var result = typeService.UpdateFlagShow(updateDto);
            return new R<bool>()
            {
                Payload = result
            };
        }

        public R<List<Shared.Modal.Entity.Type>> GetByShowInLandingPage([FromServices] ITypeService typeService)
        {
            var result = typeService.GetByShowLandingPage();
            return new R<List<Shared.Modal.Entity.Type>>()
            {
                Payload = result
            };
        }

    }
}
