using Carter;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using SNET.Framework.Api.DependencyConfig;
using SNET.Framework.Api.Metricas;
using SNET.Framework.Domain.Notifications.Email;
using SNET.Framework.Infrastructure.Notifications.Email;
using SNET.Framework.Persistence;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

builder.AddRepositories();
//builder.AddLogger();
builder.AddEmailSettings();
builder.AddAutenticationServices();

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resuorse =>
    {
        resuorse.AddService("FrameworkApi");
    })
    .WithMetrics(metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation();

        metrics.AddOtlpExporter();
    })
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddEntityFrameworkCoreInstrumentation();

        tracing.AddOtlpExporter();
    });

    builder.Logging.AddOpenTelemetry(loggin => loggin.AddOtlpExporter());

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

builder.Services.AddScoped<IEmailNotifications, SmtpNotifications>();

// Optener JwtSettings desde appsettings.json
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

// Congiguración del servicio de autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
    };
});


var app = builder.Build();

app.UseOpenApi();
app.UseSwaggerUi(settings => { settings.Path = "/docs"; });
app.UseReDoc(settings =>
{
    settings.Path = "/redoc";
    settings.DocumentPath = "/swagger/v1/swagger.json";
});

app.UseAuthentication();
app.UseAuthorization();

app.MapCarter();

app.MapGet("/{name}", (ILogger<Program> logger, string name) =>
{
    ContarNombres.Contar.Add(1, new KeyValuePair<string, object>("nombre", name));
    logger.LogInformation("Su nombre es {name}", name);
    return name;
});

app.Run();
