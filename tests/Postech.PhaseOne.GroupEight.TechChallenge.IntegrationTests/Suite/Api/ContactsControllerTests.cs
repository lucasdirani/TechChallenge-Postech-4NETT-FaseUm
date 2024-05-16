using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Infra.Data.Contexts;
using Postech.PhaseOne.GroupEight.TechChallenge.IntegrationTests.Configurations.Factories;
using Postech.PhaseOne.GroupEight.TechChallenge.IntegrationTests.Helpers;

namespace Postech.PhaseOne.GroupEight.TechChallenge.IntegrationTests.Suite.Api
{
    public class ContactsControllerTests(ContactManagementAppWebApplicationFactory factory) : IClassFixture<ContactManagementAppWebApplicationFactory>, IAsyncLifetime
    {
        private readonly HttpClient _client = factory.CreateClient();

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
        [Theory(DisplayName = "Inserting contact with success")]
        [InlineData("Tatiana", "Lima", "tatidornel@gmail.com", "974025307", "51")]
        [InlineData("Elias", "Rosa", "eliasrosa@gmail.com", "974025308", "11")]
        [InlineData("Veronica", "Freitas", "veronica@gmail.com", "974025309", "38")]
        [Trait("Action", "Controller")]
        public async Task Controller_InsertingContact_ShouldBeOk(string name,
                string lastName, string email, string phone, string areaCode)
        {
            ContactInput input = new()
            {
                AreaCode = areaCode,
                Name = name,
                Email = email,
                Phone = phone,
                LastName = lastName,
            };
            var content = ContentHelper.GetStringContent(input);
            var response = await _client.PostAsync("/contacts", content);
            var result = JsonConvert.DeserializeObject<DefaultOutput>(response.Content.ReadAsStringAsync().Result);

            Assert.NotNull(result);
            Assert.True(result.Success);
        }
    }
}