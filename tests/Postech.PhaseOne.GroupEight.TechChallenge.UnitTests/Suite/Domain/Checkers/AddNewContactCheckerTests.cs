using FluentAssertions;
using Moq;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Checkers;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.UnitTests.Fakers.Domain.Entities;

namespace Postech.PhaseOne.GroupEight.TechChallenge.UnitTests.Suite.Domain.Checkers
{
    public class AddNewContactCheckerTests
    {
        [Fact(DisplayName = "Contact already registered in the database")]
        [Trait("Action", "CheckRegisteredContactAsync")]
        public async Task CheckRegisteredContactAsync_ContactAlreadyRegistered_ShouldReturnTrue()
        {
            // Arrange
            ContactEntity contactToBeChecked = new ContactEntityFaker().Generate();
            List<ContactEntity> registeredContacts = new ContactEntityFaker().Generate(10);
            registeredContacts.Add(contactToBeChecked);
            Mock<IContactRepository> contactRepository = new();
            bool GetContactsByContactPhoneExpression(ContactEntity contact) => 
                contact.ContactPhone.Number == contactToBeChecked.ContactPhone.Number 
                && contact.ContactPhone.AreaCode.Value == contactToBeChecked.ContactPhone.AreaCode.Value;
            contactRepository
                .Setup(contact => contact.GetContactsByContactPhoneAsync(contactToBeChecked.ContactPhone))
                .ReturnsAsync(registeredContacts.Where(GetContactsByContactPhoneExpression));
            AddNewContactChecker checker = new(contactRepository.Object);

            // Act
            bool result = await checker.CheckRegisteredContactAsync(contactToBeChecked);

            // Assert
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Contact not registered in the database")]
        [Trait("Action", "CheckRegisteredContactAsync")]
        public async Task CheckRegisteredContactAsync_ContactNotRegistered_ShouldReturnFalse()
        {
            // Arrange
            ContactEntity contactToBeChecked = new ContactEntityFaker().Generate();
            List<ContactEntity> registeredContacts = new ContactEntityFaker().Generate(10);
            Mock<IContactRepository> contactRepository = new();
            bool GetContactsByContactPhoneExpression(ContactEntity contact) =>
                contact.ContactPhone.Number == contactToBeChecked.ContactPhone.Number
                && contact.ContactPhone.AreaCode.Value == contactToBeChecked.ContactPhone.AreaCode.Value;
            contactRepository
                .Setup(contact => contact.GetContactsByContactPhoneAsync(contactToBeChecked.ContactPhone))
                .ReturnsAsync(registeredContacts.Where(GetContactsByContactPhoneExpression));
            AddNewContactChecker checker = new(contactRepository.Object);

            // Act
            bool result = await checker.CheckRegisteredContactAsync(contactToBeChecked);

            // Assert
            result.Should().BeFalse();
        }
    }
}