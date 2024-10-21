using Carter;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SNET.Framework.Api.DependencyConfig;
using SNET.Framework.Api.Metrics;
using SNET.Framework.Domain.Notifications.Email;
using SNET.Framework.Infrastructure.Notifications.Email;
using SNET.Framework.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddCarter();

builder.AddRepositories();
builder.AddLogger();
builder.AddEmailSettings();

//builder.Services.AddOpenTelemetry()
//    .ConfigureResource(resources =>
//    {
//        resources.AddService(builder.Configuration["OpenTelemetry:ServiceName"]);
//    })
//    .WithMetrics(metrics=>
//    {
//        //metrics.AddMeter(
//        //    "Microsft.AspNetCore.Hosting",
//        //    "Microsoft.AspNetCore.Server.Kestrel",
//        //    "System.Net.Http");

//        metrics
//            .AddAspNetCoreInstrumentation()
//            .AddHttpClientInstrumentation();

//        metrics.AddMeter(CountHomeRequest.Metrica.Name);

//        //metrics.AddOtlpExporter(x=>x.Endpoint = new Uri("http://aspire.dashboard:18889"));
//        metrics.AddOtlpExporter()
//        .AddPrometheusExporter();
//    })
//    .WithTracing( tracing =>
//    {
//        tracing
//            .AddAspNetCoreInstrumentation()
//            .AddHttpClientInstrumentation()
//            .AddEntityFrameworkCoreInstrumentation();

//        tracing.AddOtlpExporter();
//    });

//builder.Logging.AddOpenTelemetry(logging =>
//{
//    logging.IncludeFormattedMessage = true;
//    logging.AddOtlpExporter();
//});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddMediatR(x =>
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

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseOpenApi();
app.UseSwaggerUi(settings => { settings.Path = "/docs"; });
app.UseReDoc(settings =>
{
    settings.Path = "/redoc";
    settings.DocumentPath = "/swagger/v1/swagger.json";
});

app.MapCarter();
//app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.MapGet("/", (ILogger<Program> logger) =>
{
    CountHomeRequest.Counter.Add(1,
        new KeyValuePair<string, object>("date-time", DateTime.Now.Second),
        new KeyValuePair<string, object>("date-time2", DateTime.Now.Minute));
    logger.LogInformation("Pagina inicial. {mensaje}", "hola mundo");
    return "Hello World!";
});

app.Run();
