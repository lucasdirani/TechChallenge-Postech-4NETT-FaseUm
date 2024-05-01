using Bogus;
using FluentAssertions;
using Moq;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.UnitTests.Suite.Domain.Handlers.Contacts
{
    public class UpdateContactHandlerTests
    {
        private readonly Faker _faker = new("pt_BR");

        [Theory(DisplayName = "Updating a registered contact")]
        [InlineData("29bdd56a-4fdc-4215-aecd-f00df20d4ea7")]
        [InlineData("53f59d8d-647e-4859-bca4-6569f3cc6f0a")]
        [InlineData("0fe764f7-9135-4fdb-a950-b821dd45db75")]
        [Trait("Action", "Handle")]
        public async Task Handle_UpdatingRegisteredContact_ShouldUpdateContact(Guid contactIdToUpdate)
        {
            ContactNameValueObject contactNameToUpdate = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject contactEmailToUpdate = new(_faker.Internet.Email());
            ContactPhoneValueObject contactPhoneToUpdate = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("11"));
            ContactEntity contactToUpdate = new(contactNameToUpdate, contactEmailToUpdate, contactPhoneToUpdate);

            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.GetByIdAsync(contactIdToUpdate)).ReturnsAsync(contactToUpdate);
            contactRepository.Setup(c => c.SaveChangesAsync());

            UpdateContactHandler handler = new(contactRepository.Object);         
            UpdateContactInput input = new()
            {
                ContactId = contactIdToUpdate,
                Name = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                Email = _faker.Internet.Email(),
                Phone = _faker.Phone.PhoneNumber("9########"),
                AreaCode = "11"
            };
            DefaultOutput output = await handler.Handle(input, CancellationToken.None);

            output.Success.Should().Be(true);
            output.Message.Should().NotBeNullOrEmpty();

            contactToUpdate.ContactName.FirstName.Should().Be(input.Name);
            contactToUpdate.ContactName.LastName.Should().Be(input.LastName);
            contactToUpdate.ContactEmail.Value.Should().Be(input.Email); 
            contactToUpdate.ContactPhone.Number.Should().Be(input.Phone); 
            contactToUpdate.ContactPhone.AreaCode.Value.Should().Be(input.AreaCode); 
            contactToUpdate.ModifiedAt.Should().BeOnOrBefore(DateTime.UtcNow);
            contactRepository.Verify(c => c.GetByIdAsync(contactIdToUpdate), Times.Once());
            contactRepository.Verify(c => c.SaveChangesAsync(), Times.Once());
        }

        [Theory(DisplayName = "Updating an unregistered contact")]
        [InlineData("1a7cecc2-b866-4cad-947d-14405be78616")]
        [InlineData("ecd81ba1-c2ea-4575-aa02-e9c42c9858cc")]
        [InlineData("6f4b7f4c-e1e3-4d60-81c2-c9279ce81f85")]
        [Trait("Action", "Handle")]
        public async Task Handle_UpdatingUnregisteredContact_ShouldThrowNotFoundException(Guid contactIdToUpdate)
        {
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.GetByIdAsync(contactIdToUpdate)).ReturnsAsync(() => null);
            UpdateContactHandler handler = new(contactRepository.Object);
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
