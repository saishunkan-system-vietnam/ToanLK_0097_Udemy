using EntityFrameWork.Context;
using EntityFrameWork.Filters.ActionFilters;
using EntityFrameWork.Middleware;
using EntityFrameWork.Service;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<ICountryService, CountryService>();

builder.Services.AddControllers((option) =>
{
    var service = (ILogger < ResponseHeaderActionFilter >)builder.Services.BuildServiceProvider().GetRequiredService(typeof(ILogger<ResponseHeaderActionFilter>));
    option.Filters.Add(new ResponseHeaderActionFilter(service, "keyFromGlobal", "Value from Global",3));
});

builder.Services.AddHttpLogging(o => 
{ o.LoggingFields = HttpLoggingFields.RequestBody | HttpLoggingFields.RequestMethod ; }
);

builder.Host.UseSerilog((HostBuilderContext context,IServiceProvider services, LoggerConfiguration logConfig) =>
{
    logConfig.ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services);
});

//logger
builder.Host.ConfigureLogging(loggingProvider =>
{
    loggingProvider.ClearProviders();
    loggingProvider.AddConsole();
    //loggingProvider.AddDebug();
});

builder.Services.AddDbContext<PersonDbContext>((option) =>
{
    option.UseMySQL(builder.Configuration["ConnectionStrings:DefaultConnection"])
    .UseLazyLoadingProxies().EnableSensitiveDataLogging(true);
});
var app = builder.Build();
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/person/error");
    //app.UseExceptionHandleMiddleware();
}

app.UseHttpLogging();


//app.Logger.LogDebug("test");
//app.Logger.LogInformation("test");
//app.Logger.LogWarning("test");
//app.Logger.LogError("test");
//app.Logger.LogCritical("test");


app.MapControllers();

app.Run();
