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
    public class DeleteContactHandlerTests
    {
        private readonly Faker _faker = new("pt_BR");

        [Theory(DisplayName = "Deleting a registered contact")]
        [InlineData("29bdd56a-4fdc-4215-aecd-f00df20d4ea7")]
        [InlineData("53f59d8d-647e-4859-bca4-6569f3cc6f0a")]
        [InlineData("0fe764f7-9135-4fdb-a950-b821dd45db75")]
        [Trait("Action", "Handle")]
        public async Task Handle_DeletingRegisteredContact_ShouldDeleteContact(Guid contactIdThatWillBeDeleted)
        {
            // Arrange
            ContactNameValueObject contactNameToBeDeleted = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject contactEmailToBeDeleted = new(_faker.Internet.Email());
            ContactPhoneValueObject contactPhoneToBeDeleted = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("11"));
            ContactEntity contactToBeDeleted = new(contactNameToBeDeleted, contactEmailToBeDeleted, contactPhoneToBeDeleted);
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.GetByIdAsync(contactIdThatWillBeDeleted)).ReturnsAsync(contactToBeDeleted);
            contactRepository.Setup(c => c.SaveChangesAsync());
            DeleteContactHandler handler = new(contactRepository.Object);
            DeleteContactInput input = new() { ContactId = contactIdThatWillBeDeleted };

            // Act
            DefaultOutput output = await handler.Handle(input, CancellationToken.None);

            // Assert
            output.Success.Should().Be(true);
            output.Message.Should().NotBeNullOrEmpty();
            contactToBeDeleted.IsActive().Should().BeFalse();
            contactToBeDeleted.ModifiedAt.Should().BeOnOrBefore(DateTime.UtcNow);
            contactRepository.Verify(c => c.GetByIdAsync(contactIdThatWillBeDeleted), Times.Once());
            contactRepository.Verify(c => c.SaveChangesAsync(), Times.Once());
        }

        [Theory(DisplayName = "Deleting an unregistered contact")]
        [InlineData("1a7cecc2-b866-4cad-947d-14405be78616")]
        [InlineData("ecd81ba1-c2ea-4575-aa02-e9c42c9858cc")]
        [InlineData("6f4b7f4c-e1e3-4d60-81c2-c9279ce81f85")]
        [Trait("Action", "Handle")]
        public async Task Handle_DeletingUnegisteredContact_ShouldThrowNotFoundException(Guid contactIdThatWillBeDeleted)
        {
            // Arrange
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.GetByIdAsync(contactIdThatWillBeDeleted)).ReturnsAsync(() => null);
            DeleteContactHandler handler = new(contactRepository.Object);
            DeleteContactInput input = new() { ContactId = contactIdThatWillBeDeleted };

            // Assert
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(input, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
            contactRepository.Verify(c => c.GetByIdAsync(contactIdThatWillBeDeleted), Times.Once());
            contactRepository.Verify(c => c.SaveChangesAsync(), Times.Never());
        }

        [Theory(DisplayName = "Deleting a contact that has previously been deleted")]
        [InlineData("1a7cecc2-b866-4cad-947d-14405be78616")]
        [InlineData("ecd81ba1-c2ea-4575-aa02-e9c42c9858cc")]
        [InlineData("6f4b7f4c-e1e3-4d60-81c2-c9279ce81f85")]
        [Trait("Action", "Handle")]
        public async Task Handle_DeletingContactThatHasPreviouslyBeenDeleted_ShouldThrowEntityInactiveException(Guid contactIdThatWillBeDeleted)
        {
            // Arrange
            ContactNameValueObject contactNameToBeDeleted = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject contactEmailToBeDeleted = new(_faker.Internet.Email());
            ContactPhoneValueObject contactPhoneToBeDeleted = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("21"));
            ContactEntity contactToBeDeleted = new(contactNameToBeDeleted, contactEmailToBeDeleted, contactPhoneToBeDeleted);
            contactToBeDeleted.Inactivate();
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.GetByIdAsync(contactIdThatWillBeDeleted)).ReturnsAsync(contactToBeDeleted);
            DeleteContactHandler handler = new(contactRepository.Object);
            DeleteContactInput input = new() { ContactId = contactIdThatWillBeDeleted };

            // Assert
            EntityInactiveException exception = await Assert.ThrowsAsync<EntityInactiveException>(() => handler.Handle(input, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
            contactRepository.Verify(c => c.GetByIdAsync(contactIdThatWillBeDeleted), Times.Once());
            contactRepository.Verify(c => c.SaveChangesAsync(), Times.Never());
        }
    }
}