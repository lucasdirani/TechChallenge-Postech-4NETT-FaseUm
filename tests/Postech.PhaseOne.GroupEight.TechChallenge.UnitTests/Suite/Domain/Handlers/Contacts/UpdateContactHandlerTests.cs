using Bogus;
using FluentAssertions;
using Moq;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Factories.Interfaces;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.UnitTests.Suite.Domain.Handlers.Contacts
{
    public class UpdateContactHandlerTests 
    {
        private readonly Faker _faker = new("pt_BR");

        [Fact(DisplayName = "Updating a registered contact")]
        [Trait("Action", "Handle")]
        public async Task Handle_UpdatingRegisteredContact_ShouldUpdateContact()
        {
            var contactIdToUpdate = _faker.Random.Guid();

            ContactNameValueObject contactNameToUpdate = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject contactEmailToUpdate = new(_faker.Internet.Email());
            ContactPhoneValueObject contactPhoneToUpdate = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("11"));
            ContactEntity contactToUpdate = new(contactNameToUpdate, contactEmailToUpdate, contactPhoneToUpdate);

            ContactPhoneValueObject newContact = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("11"));
            UpdateContactInput input = new()
            {
                ContactId = contactIdToUpdate,
                Name = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                Email = _faker.Internet.Email(),
                Phone = newContact.Number,
                AreaCode = newContact.AreaCode.Value,
                IsActive = true
            };
           

            Mock<IContactRepository> contactRepository = new();
            Mock<IContactPhoneValueObjectFactory> ContactPhoneValueObjectFactory = new();
            ContactPhoneValueObjectFactory.Setup(c => c.CreateAsync(newContact.Number, newContact.AreaCode.Value)).ReturnsAsync(newContact);
            contactRepository.Setup(c => c.GetByIdAsync(contactIdToUpdate)).ReturnsAsync(contactToUpdate);
            contactRepository.Setup(c => c.SaveChangesAsync());

            UpdateContactHandler handler = new(contactRepository.Object, ContactPhoneValueObjectFactory.Object);         
            DefaultOutput output = await handler.Handle(input, CancellationToken.None);

            output.Success.Should().Be(true);
            output.Message.Should().NotBeNullOrEmpty();

            contactToUpdate.ContactName.FirstName.Should().Be(input.Name);
            contactToUpdate.ContactName.LastName.Should().Be(input.LastName);
            contactToUpdate.ContactEmail.Value.Should().Be(input.Email); 
            contactToUpdate.ContactPhone.Number.Should().Be(input.Phone); 
            contactToUpdate.ContactPhone.AreaCode.Value.Should().Be(input.AreaCode); 
            contactToUpdate.ModifiedAt.Should().BeOnOrBefore(DateTime.UtcNow);
            contactToUpdate.Active.Should().Be(input.IsActive);
            contactRepository.Verify(c => c.GetByIdAsync(contactIdToUpdate), Times.Once());
            contactRepository.Verify(c => c.SaveChangesAsync(), Times.Once());
        }

        [Fact(DisplayName = "Updating an unregistered contact")]
        [Trait("Action", "Handle")]
        public async Task Handle_UpdatingUnregisteredContact_ShouldThrowNotFoundException()
        {
            var contactIdToUpdate = _faker.Random.Guid();
            Mock<IContactRepository> contactRepository = new();
            Mock<IContactPhoneValueObjectFactory> ContactPhoneValueObjectFactory = new();
            contactRepository.Setup(c => c.GetByIdAsync(contactIdToUpdate)).ReturnsAsync(() => null);
            UpdateContactHandler handler = new(contactRepository.Object, ContactPhoneValueObjectFactory.Object);
            UpdateContactInput input = new()
            {
                ContactId = contactIdToUpdate,
                Name = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                Email = _faker.Internet.Email(),
                Phone = _faker.Phone.PhoneNumber("9########"),
                AreaCode = "11"
            };
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(input, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
            contactRepository.Verify(c => c.GetByIdAsync(contactIdToUpdate), Times.Once());
            contactRepository.Verify(c => c.SaveChangesAsync(), Times.Never());
        }
    }
}
