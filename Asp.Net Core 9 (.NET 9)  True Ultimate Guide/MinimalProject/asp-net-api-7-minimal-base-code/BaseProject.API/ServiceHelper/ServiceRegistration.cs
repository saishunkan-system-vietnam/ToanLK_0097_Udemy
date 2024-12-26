using BaseProject.Core.Service;
using BaseProject.Core;
using BaseProject.Infrastructure;
using System.Runtime.CompilerServices;
using AutoMapper;
using BaseProject.Shared.Entity;
using Microsoft.Extensions.DependencyInjection;
using BaseProject.Core.AutoMapper;
using BaseProject.API.Auth;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using BaseProject.Core.Mail;
using BaseProject.Core.Token;
using BaseProject.Core.SignalR;

namespace BaseProject.API.ServiceHelper
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterService(this IServiceCollection service)
        {
            // AutoMapper
            service.AddAutoMapper(typeof(ProfileAutoMapperConfig));

            // ignore null value from response 
            service.Configure<JsonOptions>(option =>
                option.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault | JsonIgnoreCondition.WhenWritingNull
            );

            // register generic repository pattern
            service.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            service.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));

            // register service here
            service.AddTransient<IProductService,ProductService>();
            service.AddTransient<IAccountService, AccountService>();
            service.AddTransient<ITypeService, TypeService>();
            service.AddTransient<ISettingService, SettingService>();
            service.AddTransient<ICartService, CartService>();
            service.AddTransient<INewsService, NewsService>();

            // Unit of work
            service.AddTransient<IUnitOfWork, UnitOfWork>();

            //
            service.AddSingleton<CommentHub , CommentHub> ();

            // token service
            service.AddSingleton<TokenService>();

            //mail sender service
            service.AddScoped<IMailSender, MailSender>();
            return service;
        }
    }
}
