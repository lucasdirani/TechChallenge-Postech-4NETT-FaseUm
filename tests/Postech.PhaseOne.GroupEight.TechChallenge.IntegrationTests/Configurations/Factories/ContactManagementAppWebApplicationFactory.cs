using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Postech.PhaseOne.GroupEight.TechChallenge.Infra.Data.Contexts;

namespace Postech.PhaseOne.GroupEight.TechChallenge.IntegrationTests.Configurations.Factories
{
    public class ContactManagementAppWebApplicationFactory(string connectionString) : WebApplicationFactory<Program>
    {
        public readonly string ConnectionString = connectionString;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                ServiceDescriptor? dbContextServiceDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(DbContextOptions<ContactManagementDbContext>));
                if (dbContextServiceDescriptor is not null)
                {
                    services.Remove(dbContextServiceDescriptor);
                }
                services.AddDbContext<ContactManagementDbContext>((provider, optionsBuilder) =>
                {
                    optionsBuilder.UseNpgsql(ConnectionString, npgsqlBuilder =>
                    {
                        npgsqlBuilder.EnableRetryOnFailure(3);
                    });
                });
                ServiceProvider serviceProvider = services.BuildServiceProvider();
                using IServiceScope scope = serviceProvider.CreateScope();
                ContactManagementDbContext db = scope.ServiceProvider.GetRequiredService<ContactManagementDbContext>();
                db.Database.EnsureCreated();
            });
        }
    }
}