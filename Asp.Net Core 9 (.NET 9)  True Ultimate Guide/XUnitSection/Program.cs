using XUnitSection.Services.IService;
using XUnitSection.Services.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
