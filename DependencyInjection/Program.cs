using Autofac;
using Autofac.Extensions.DependencyInjection;
using DependencyInjection.Controllers;
using DependencyInjection.Services.IService;
using DependencyInjection.Services.Service;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// AutoFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<SongService>().As<ISongService>().InstancePerDependency();
});

// IoC Registraction
//builder.Services.Add(
//    new ServiceDescriptor(typeof(ISongService), typeof(SongService),ServiceLifetime.Scoped)
//);


//1.DIP high level not directly call low level, should using abstract class or interface 
//2. interface (abtraction) should not depend on detail, classes communicate with each other using interface (not implementation)

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();


app.Run();
