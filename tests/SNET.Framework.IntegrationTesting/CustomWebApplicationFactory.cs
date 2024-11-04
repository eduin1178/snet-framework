using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SNET.Framework.Persistence;

namespace SNET.Framework.IntegrationTesting;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Eliminar la configuración de base de datos existente
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApiDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Añadir base de datos en memoria para pruebas
            services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryTestDb");
            });

            // Crear un proveedor de servicios para aplicar la configuración
            var sp = services.BuildServiceProvider();

            // Crear el scope y aplicar migraciones a la base de datos en memoria
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
            db.Database.EnsureCreated();


        });
    }
}
