using Bogus;
using FluentAssertions;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;
using Postech.PhaseOne.GroupEight.TechChallenge.IntegrationTests.Configurations.Base;
using Postech.PhaseOne.GroupEight.TechChallenge.IntegrationTests.Fixtures;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Postech.PhaseOne.GroupEight.TechChallenge.IntegrationTests.Suite.Api
{
    [Collection("Integration Tests")]
    public class ContactsControllerTests(IntegrationTestFixture fixture) : IntegrationTestBase(fixture)
    {
        private readonly Faker _faker = new("pt_BR");

        [Theory(DisplayName = "Add an new contact with success")]
        [InlineData("Tatiana", "Lima", "tatidornel@gmail.com", "974025307", "51")]
        [InlineData("Elias", "Rosa", "eliasrosa@gmail.com", "974025308", "11")]
        [InlineData("Veronica", "Freitas", "veronica@gmail.com", "974025309", "38")]
        [Trait("Action", "Contacts")]
        public async Task AddContactEndpoint_AddAnNewContact_ShouldAddContact(
            string contactFirstName,
            string contactLastName,
            string contactEmail,
            string contactPhoneNumber,
            string contactPhoneNumberAreaCode)
        {
            // Arrange
            AddContactInput addContactInput = new()
            {
                ContactPhoneNumberAreaCode = contactPhoneNumberAreaCode,
                ContactFirstName = contactFirstName,
                ContactEmail = contactEmail,
                ContactPhoneNumber = contactPhoneNumber,
                ContactLastName = contactLastName,
            };

            // Act
            using HttpResponseMessage responseMessage = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/contacts")
            {
                Content = new StringContent(JsonSerializer.Serialize(addContactInput), Encoding.UTF8, "application/json")
            });

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            DefaultOutput? responseMessageContent = JsonSerializer.Deserialize<DefaultOutput>(await responseMessage.Content.ReadAsStringAsync());
            responseMessageContent.Should().NotBeNull();
            responseMessageContent?.Message.Should().NotBeNullOrEmpty();
            responseMessageContent?.Success.Should().BeTrue();
        }

        [Fact]
        [Trait("Action", "Contacts")]
        public async Task UpdateContactEndpoint_UpdateAnExistingContact_ShouldUpdateTheContact()
        {
            // Arrange            
            IContactRepository contactRepository = GetService<IContactRepository>();
            ContactNameValueObject contactName = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject contactEmail = new(_faker.Internet.Email());
            AreaCodeValueObject areaCode = await contactRepository.GetAreaCodeByValueAsync("11");
            ContactPhoneValueObject contactPhone = new(_faker.Phone.PhoneNumber("9########"), areaCode);
            ContactEntity contactEntity = new(contactName, contactEmail, contactPhone);
            await contactRepository.InsertAsync(contactEntity);
            await contactRepository.SaveChangesAsync();
            UpdateContactInput updateContactInput = new()
            {
                ContactId = contactEntity.Id,
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "31",
                IsActive = _faker.Random.Bool()
            };

            // Act
            using HttpResponseMessage responseMessage = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Put, "/contacts")
            {
                Content = new StringContent(JsonSerializer.Serialize(updateContactInput), Encoding.UTF8, "application/json")
            });

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            DefaultOutput? responseMessageContent = JsonSerializer.Deserialize<DefaultOutput>(await responseMessage.Content.ReadAsStringAsync());
            responseMessageContent.Should().NotBeNull();
            responseMessageContent?.Message.Should().NotBeNullOrEmpty();
            responseMessageContent?.Success.Should().BeTrue();
        }

        [Fact]
        [Trait("Action", "Contacts")]
        public async Task DeleteContactEndpoint_DeleteAnExistingContact_ShouldDeleteTheContact()
        {
            // Arrange
            IContactRepository contactRepository = GetService<IContactRepository>();
            ContactNameValueObject contactName = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject contactEmail = new(_faker.Internet.Email());
            AreaCodeValueObject areaCode = await contactRepository.GetAreaCodeByValueAsync("11");
            ContactPhoneValueObject contactPhone = new(_faker.Phone.PhoneNumber("9########"), areaCode);
            ContactEntity contactEntity = new(contactName, contactEmail, contactPhone);
            await contactRepository.InsertAsync(contactEntity);
            await contactRepository.SaveChangesAsync();
            DeleteContactInput input = new() { ContactId = contactEntity.Id };

            // Act
            using HttpResponseMessage responseMessage = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, "/contacts")
            {
                Content = new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, "application/json")
            });

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            DefaultOutput? responseMessageContent = JsonSerializer.Deserialize<DefaultOutput>(await responseMessage.Content.ReadAsStringAsync());
            responseMessageContent.Should().NotBeNull();
            responseMessageContent?.Message.Should().NotBeNullOrEmpty();
            responseMessageContent?.Success.Should().BeTrue();
        }
    }
}