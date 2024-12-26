using RoutingSection.CustomRouteConstraints;
using UdemyNet8.CustomModelBinder;

// create app
var builder = WebApplication.CreateBuilder(args);

#region CONTROLLER SECTION

builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new BookCustomBinderProvider());
});
// builder.Services.AddControllers().AddXmlSerializerFormatters();


#endregion

#region ROUTE SECTION
// add custom route constraint
builder.Services.AddRouting(options => {
    options.ConstraintMap.Add("monthConstraint", typeof(MonthRouteConstraint));
});
#endregion

// build app
var app = builder.Build();

// HTTP SECTION
//MainHttp.Program(app);

// MIDDLEWARE SECTION
//MainMiddleware.Program(app);

// ROUTING SECTION
//MainRouting.Program(app);

// CONTROLLER SECTION
//MainController.Program(app);

// terminal middleware: static file
//app.UseStaticFiles();

// add all controllers to endpoint
app.MapControllers();

// run app
app.Run();