using Carter;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SNET.Framework.Api.DependencyConfig;
using SNET.Framework.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

builder.AddRepositories();

builder.Services.AddMediatR(x=>
{
    x.RegisterServicesFromAssembly(typeof(SNET.Framework.Features.AssemblyReference).Assembly);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(c =>
{
    c.Title = "Framework Api";
    c.Version = "v1";
});

builder.Services.AddDbContext<ApiDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApiDbContext"));
}, ServiceLifetime.Scoped);

builder.Services.AddValidatorsFromAssembly(typeof(SNET.Framework.Features.AssemblyReference).Assembly, ServiceLifetime.Scoped);

var app = builder.Build();

app.UseOpenApi();
app.UseSwaggerUi(settings => { settings.Path = "/docs"; });
app.UseReDoc(settings =>
{
    settings.Path = "/redoc";
    settings.DocumentPath = "/swagger/v1/swagger.json";
});

app.MapCarter();

app.MapGet("/", () => "Hello World!");

app.Run();
