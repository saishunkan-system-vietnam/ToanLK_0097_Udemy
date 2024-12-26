using EnvironmentConfiguration.Model;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    Console.WriteLine("running in development");
}
else if (app.Environment.IsStaging())
{
    Console.WriteLine("running in Staging");
}
else if (app.Environment.IsProduction())
{
    Console.WriteLine("running in Producting");
}

app.MapGet("/", () => "Hello World!");

// can get and set
var configKey = app.Configuration["config-key"];
// get value with specific type (only simple type such a string, int,...)
var configBook = app.Configuration.GetValue<Book>("config-book");
// if we want to get complex value, using getSection
Book? configModel = app.Configuration.GetSection("TimeOut").Get<Book>();
// we can step thought nested json object
var childVal = app.Configuration["NestedConfig:ChildValue:Test"];

Book ExistObject = null;
// we can also using Bind()
app.Configuration.GetSection("config-key").Bind(ExistObject);

app.UseRouting();
app.MapControllers();
app.Run();
