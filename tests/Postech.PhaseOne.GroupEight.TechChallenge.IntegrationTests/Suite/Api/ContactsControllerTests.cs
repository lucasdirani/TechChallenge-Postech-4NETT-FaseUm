using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Postech.PhaseOne.GroupEight.TechChallenge.Infra.Data.Contexts;
using Postech.PhaseOne.GroupEight.TechChallenge.IntegrationTests.Configurations.Factories;

namespace Postech.PhaseOne.GroupEight.TechChallenge.IntegrationTests.Suite.Api
{
    public class ContactsControllerTests(ContactManagementAppWebApplicationFactory factory) : IClassFixture<ContactManagementAppWebApplicationFactory>, IAsyncLifetime
    {
        private readonly ContactManagementAppWebApplicationFactory _factory = factory;

        public async Task DisposeAsync()
        {
            await _factory.DisposeAsync();
        }

        public async Task InitializeAsync()
        {
            await _factory.InitializeContainerAsync();
            using IServiceScope scope = _factory.Services.CreateScope();
            ContactManagementDbContext context = scope.ServiceProvider.GetRequiredService<ContactManagementDbContext>();
            await context.Database.MigrateAsync();
        }
    }
}