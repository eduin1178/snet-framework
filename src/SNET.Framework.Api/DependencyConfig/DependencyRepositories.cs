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
    }
}
