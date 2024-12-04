using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using SNET.Framework.Domain.Authentications;
using SNET.Framework.Domain.Notifications.Email;
using SNET.Framework.Domain.Repositories;
using SNET.Framework.Domain.UnitOfWork;
using SNET.Framework.Infrastructure.Authentications;
using SNET.Framework.Persistence.Repositories;
using SNET.Framework.Persistence.UnitOfWork;
using System.Text;

namespace SNET.Framework.Api.DependencyConfig
{
    public static class DependencyRepositories
    {
        public static void AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IManagerToken, ManagerToken>();
        }

        public static void AddLogger(this WebApplicationBuilder builder)
        {

            builder.Host.UseSerilog();

            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
               .Enrich.FromLogContext()
               .WriteTo.Console()
               .CreateLogger();
        }

        public static void AddEmailSettings(this WebApplicationBuilder builder)
        {
            var emailSettings = new EmailNotificationSettings();
            builder.Configuration.Bind("EmailNotificationSettings", emailSettings);
            builder.Services.AddSingleton(emailSettings);
        }

        public static void AddAutenticationServices(this WebApplicationBuilder builder) {

            // Agrega el servicio de autorización
            builder.Services.AddAuthorization();

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
        }
    }
}
