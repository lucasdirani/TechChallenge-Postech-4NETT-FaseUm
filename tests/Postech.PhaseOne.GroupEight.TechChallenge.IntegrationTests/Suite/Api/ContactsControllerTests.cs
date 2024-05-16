﻿using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;
using Postech.PhaseOne.GroupEight.TechChallenge.Infra.Data.Contexts;
using Postech.PhaseOne.GroupEight.TechChallenge.IntegrationTests.Configurations.Factories;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Postech.PhaseOne.GroupEight.TechChallenge.IntegrationTests.Suite.Api
{
    public class ContactsControllerTests(ContactManagementAppWebApplicationFactory factory) : IClassFixture<ContactManagementAppWebApplicationFactory>, IAsyncLifetime
    {
        private readonly ContactManagementAppWebApplicationFactory _factory = factory;
        private readonly Faker _faker = new("pt_BR");

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

        [Fact]
        public async Task UpdateContactEndpoint_UpdateAnExistingContact_ShouldUpdateTheContact()
        {
            // Arrange
            using IServiceScope scope = _factory.Services.CreateScope();
            IContactRepository contactRepository = scope.ServiceProvider.GetRequiredService<IContactRepository>();
            ContactNameValueObject contactName = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject contactEmail = new(_faker.Internet.Email());
            AreaCodeValueObject areaCode = await contactRepository.GetAreaCodeByValueAsync("11");
            ContactPhoneValueObject contactPhone = new(_faker.Phone.PhoneNumber("9########"), areaCode);
            ContactEntity contactEntity = new(contactName, contactEmail, contactPhone);
            await contactRepository.InsertAsync(contactEntity);
            await contactRepository.SaveChangesAsync();

            var updateContactInput = new UpdateContactInput
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
            using HttpClient httpClient = _factory.CreateClient();
            var Content = new StringContent(JsonSerializer.Serialize(updateContactInput), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync("/contacts", Content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Should().NotBeNull();
        }
    }
}