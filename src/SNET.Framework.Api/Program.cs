using Microsoft.EntityFrameworkCore;
using SNET.Framework.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddDbContext<ApiDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(""));
});

app.MapGet("/", () => "Hello World!");

app.Run();
