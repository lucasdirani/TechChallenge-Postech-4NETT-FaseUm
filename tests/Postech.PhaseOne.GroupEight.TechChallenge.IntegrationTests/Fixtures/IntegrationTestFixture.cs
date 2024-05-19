using Postech.PhaseOne.GroupEight.TechChallenge.IntegrationTests.Configurations.Factories;

namespace Postech.PhaseOne.GroupEight.TechChallenge.IntegrationTests.Fixtures
{
    public class IntegrationTestFixture : IAsyncLifetime
    {
        public ContactManagementAppWebApplicationFactory WebApplicationFactory { get; private set; }
        public static string ConnectionString => TestContainerFactory.ConnectionString;

        public async Task InitializeAsync()
        {
            await TestContainerFactory.EnsureInitialized();
            WebApplicationFactory = new(ConnectionString);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}