using BaseProject.API.ServiceHelper;
using BaseProject.Core.Mail;
using BaseProject.Core.SignalR;
using BaseProject.Infrastructure;
using BaseProject.Shared;
using BaseProject.Shared.Modal.Enum;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            //policy.WithOrigins("http://localhost:5173")
            policy
            //.WithOrigins("http://localhost:5173", "http://localhost:9000")
            //.WithHeaders("Authorization", "origin", "accept", "content-type")
            //.WithMethods("GET", "POST", "PUT", "DELETE")
            //.SetIsOriginAllowed((host) => true)
            //.AllowCredentials();

            .WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

builder.Services.AddMemoryCache();


builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration logConfig) =>
{
    logConfig.ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services);
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

// add s3 
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
//builder.Services.AddAWSService<IAmazonS3>();

// Register Module
builder.Services.RegisterModules();

// Create secretKey
var secretKey = BaseProject.Core.Token.TokenHandler.GenerateSecretByte();

// config authentication
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});
// config authorization
builder.Services.AddAuthorization(options =>
{
    // custom authorization filter
    options.AddPolicy(AccountRole.Admin, policy => policy.RequireRole(AccountRole.Admin));
    options.AddPolicy(AccountRole.Customer, policy => policy.RequireRole(AccountRole.Customer));
});

// add signal R config
builder.Services.AddSignalR();

// DbContext registration 
var connectionString = builder.Configuration["ConnectionStrings:default"];

var clientOrigin = builder.Configuration["Client:Origin"];

builder.Services.AddDbContext<DBMasterContext>(option =>
{
    option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
    .EnableSensitiveDataLogging(true);
    //.UseLazyLoadingProxies();
});

builder.Services.AddDbContext<DBSlaveContext>(option =>
{
    option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
    //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
    .EnableSensitiveDataLogging(true);
    //.UseLazyLoadingProxies();
});


//builder.Services.AddControllers().AddNewtonsoftJson(op => op)
// config mail sender
var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<MailConfiguration>();

builder.Services.AddSingleton(emailConfig);


// register service
builder.Services.RegisterService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Register middleware with a custom cache duration
app.UseMiddleware<ResponseCachingMiddleware>(TimeSpan.FromMinutes(5));

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);
app.UseEndpoints(endpoints => endpoints.MapHub<CommentHub>("/comment"));



app.UseHttpsRedirection();
app.MapEndpoints();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.Run();
