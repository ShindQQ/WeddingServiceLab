using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using ShoShoppers.API.CustomMiddleware;
using System.Reflection;
using WeddingService.Bll.Services;
using WeddingService.Bll.Services.Interfaces;
using WeddingService.Dal.Contexts;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddControllersWithViews(configure => { configure.ReturnHttpNotAcceptable = true; })
    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.SwaggerDoc("WeddingServiceApiSpecification", new OpenApiInfo
    {
        Title = "Wedding services API",
        Description = "Using this api you can operate with services and orders"
    });

    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    setupAction.IncludeXmlComments(xmlCommentsFullPath);
});

builder.Services.AddScoped<ICarsService, CarsService>();
builder.Services.AddScoped<IClothesService, ClothesService>();
builder.Services.AddScoped<ICeremoniesService, CeremoniesService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<WeddingServiceContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setupAction =>
    {
        setupAction.SwaggerEndpoint("/swagger/WeddingServiceApiSpecification/swagger.json", "Wedding services API");
    });
}

app.UseRouting();

app.UseMiddleware<HttpStatusCodeCustomExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
