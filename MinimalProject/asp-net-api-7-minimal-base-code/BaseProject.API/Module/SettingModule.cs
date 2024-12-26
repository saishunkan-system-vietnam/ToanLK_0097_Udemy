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
using System.Text.RegularExpressions;

namespace BaseProject.API.Module
{
    public class SettingModule : IModule
    {

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            return builder;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            var setting = endpoints.MapGroup("/setting")
                        .WithTags("setting");
            
            setting.MapGet("/", GetAll);
            setting.MapPost("/", AddSetting);


            return endpoints;
        }
        
        public R<List<Setting>> GetAll([FromServices] ISettingService settingService)
        {
            var result = settingService.Get().ToList();
            return new R<List<Setting>>()
            {
                Payload = result
            };
        }

        public R<bool> AddSetting(AddLandingPageSlideImageDto slideShowImages, [FromServices] ISettingService settingService) {
            var result = settingService.AddSetting(slideShowImages.Image);

            return new R<bool>() { Payload =  true };
        }
     
    }
}
