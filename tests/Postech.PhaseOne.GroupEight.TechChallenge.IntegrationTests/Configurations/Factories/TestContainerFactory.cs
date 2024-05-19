using Testcontainers.PostgreSql;

namespace Postech.PhaseOne.GroupEight.TechChallenge.IntegrationTests.Configurations.Factories
{
    public static class TestContainerFactory
    {
        private static PostgreSqlContainer? _container;
        private static readonly Lazy<Task> _initializeTask = new(InitializeAsync);

        public static string ConnectionString { get; private set; }

        public static async Task InitializeAsync()
        {
            if (_container is null)
            {
                _container = new PostgreSqlBuilder()
                    .WithDatabase("ContactManagementDB")
                    .WithUsername("admin")
                    .WithPassword("123456")
                    .WithPortBinding("5433", "5432")
                    .Build();
                await _container.StartAsync();
                ConnectionString = _container.GetConnectionString();
            }
        }

        public static Task EnsureInitialized() => _initializeTask.Value;

        public static async Task DisposeAsync()
        {
            if (_container is not null)
            {
                await _container.StopAsync();
                await _container.DisposeAsync();
                _container = null;
            }
        }
    }
}