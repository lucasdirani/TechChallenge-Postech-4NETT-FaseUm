using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Postech.PhaseOne.GroupEight.TechChallenge.Infra.Data.Contexts;
using Testcontainers.PostgreSql;

namespace Postech.PhaseOne.GroupEight.TechChallenge.IntegrationTests.Configurations.Factories
{
    public class ContactManagementAppWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly PostgreSqlContainer _container;

        public ContactManagementAppWebApplicationFactory()
        {
            _container = new PostgreSqlBuilder()
                .WithDatabase("ContactManagementDB")
                .WithUsername("admin")
                .WithPassword("123456")
                .WithPortBinding("5433", "5432")
                .Build();
        }

        public async Task InitializeContainerAsync()
        {
            await _container.StartAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                ServiceDescriptor? dbContextServiceDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(DbContextOptions<ContactManagementDbContext>));
                if (dbContextServiceDescriptor is not null)
                {
                    services.Remove(dbContextServiceDescriptor);
                }
                string connectionString = _container.GetConnectionString();
                services.AddDbContext<ContactManagementDbContext>((provider, optionsBuilder) =>
                {
                    optionsBuilder.UseNpgsql(connectionString, npgsqlBuilder =>
                    {
                        npgsqlBuilder.EnableRetryOnFailure(3);
                    });
                });
            });
        }
    }
}