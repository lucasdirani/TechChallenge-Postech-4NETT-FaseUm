using Microsoft.Extensions.DependencyInjection;
using Postech.PhaseOne.GroupEight.TechChallenge.IntegrationTests.Configurations.Factories;
using Postech.PhaseOne.GroupEight.TechChallenge.IntegrationTests.Fixtures;

namespace Postech.PhaseOne.GroupEight.TechChallenge.IntegrationTests.Configurations.Base
{
    public abstract class IntegrationTestBase : IClassFixture<IntegrationTestFixture>
    {
        protected readonly HttpClient HttpClient;
        protected readonly ContactManagementAppWebApplicationFactory WebApplicationFactory;

        protected IntegrationTestBase(IntegrationTestFixture fixture)
        {
            WebApplicationFactory = fixture.WebApplicationFactory;
            HttpClient = WebApplicationFactory.CreateClient();
        }

        protected T GetService<T>() 
            where T : notnull
        {
            IServiceScope scope = WebApplicationFactory.Services.CreateScope();
            return scope.ServiceProvider.GetRequiredService<T>();
        }
    }
}