using Serilog;
using Serilog.Events;
using SNET.Framework.Domain.Notifications.Email;
using SNET.Framework.Domain.Repositories;
using SNET.Framework.Domain.UnitOfWork;
using SNET.Framework.Persistence.Repositories;
using SNET.Framework.Persistence.UnitOfWork;

namespace SNET.Framework.Api.DependencyConfig
{
    public static class DependencyRepositories
    {
        public static void AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
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
    }
}
