using Bogus;
using FluentAssertions;
using Moq;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Checkers.Interfaces;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Factories.Interfaces;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ViewModels;

namespace Postech.PhaseOne.GroupEight.TechChallenge.UnitTests.Suite.Domain.Handlers.Contacts
{
    public class AddNewContactHandlerTests
    {
        private readonly Faker _faker = new("pt_BR");

        [Fact(DisplayName = "Registering a new contact")]
        [Trait("Action", "Handle")]
        public async Task Handle_NewContactRegistration_ShouldRegisterTheContact()
        {
            // Arrange
            AddContactInput addContactInput = new()
            {
                
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "11",
            };
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            contactPhoneFactory
                .Setup(c => c.CreateAsync(addContactInput.ContactPhoneNumber, addContactInput.ContactPhoneNumberAreaCode))
                .ReturnsAsync(new ContactPhoneValueObject(addContactInput.ContactPhoneNumber, AreaCodeValueObject.Create(addContactInput.ContactPhoneNumberAreaCode)));
            Mock<IRegisteredContactChecker> registeredContactChecker = new();
            registeredContactChecker.Setup(r => r.CheckRegisteredContactAsync(It.Is<ContactEntity>(contact => contact != null))).ReturnsAsync(false);
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.InsertAsync(It.Is<ContactEntity>(contact => contact != null)));
            contactRepository.Setup(c => c.SaveChangesAsync());
            AddNewContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object, registeredContactChecker.Object);

            // Act
            DefaultOutput<AddNewContactViewModel> output = await handler.Handle(addContactInput, CancellationToken.None);

            // Assert
            output.Success.Should().BeTrue();
            output.Message.Should().NotBeNullOrEmpty();
            output.Data.Should().NotBeNull();
        }
     
        [Theory(DisplayName = "Register a contact with their first name in an invalid format")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("B54no")]
        [InlineData("L!c?s")]
        [InlineData("Ta*tiana")]
        [Trait("Action", "Handle")]
        public async Task Handle_RegisterContactWithFirstNameInAnInvalidFormat_ShouldThrowContactFirstNameException(string invalidFirstName)
        {
            // Arrange
            AddContactInput addContactInput = new()
            {
                
                ContactFirstName = invalidFirstName,
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "12",
            };
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            Mock<IRegisteredContactChecker> registeredContactChecker = new();
            Mock<IContactRepository> contactRepository = new();
            AddNewContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object, registeredContactChecker.Object);

            // Assert
            ContactFirstNameException exception = await Assert.ThrowsAsync<ContactFirstNameException>(() => handler.Handle(addContactInput, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.FirstNameValue.Should().Be(invalidFirstName);            
        }

        [Theory(DisplayName = "Register a contact with their last name in an invalid format")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("[ontarroy@s")]
        [InlineData( "Lim,")]
        [InlineData("Silva ")]
        [InlineData("Alves Gom^s")]
        [Trait("Action", "Handle")]
        public async Task Handle_RegisterContactWithLastNameInAnInvalidFormat_ShouldThrowContactLastNameException(string invalidLastName)
        {
            // Arrange
            AddContactInput addContactInput = new()
            {            
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = invalidLastName,
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "12",
            };
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            Mock<IRegisteredContactChecker> registeredContactChecker = new();
            Mock<IContactRepository> contactRepository = new();
            AddNewContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object, registeredContactChecker.Object);

            // Assert
            ContactLastNameException exception = await Assert.ThrowsAsync<ContactLastNameException>(() => handler.Handle(addContactInput, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.LastNameValue.Should().Be(invalidLastName);          
        }

        [Theory(DisplayName = "Register a contact with their email address in an invalid format")]
        [InlineData("cleiton dias@gmail.com")]
        [InlineData("jair.raposo@")]
        [InlineData("milton.morgado4@hotmail")]
        [InlineData("leticia-mariagmail.com")]
        [InlineData("@yahoo.com")]
        [InlineData("")]
        [InlineData(" ")]
        [Trait("Action", "Handle")]
        public async Task Handle_RegisterContactWithEmailAddressInAnInvalidFormat_ShouldThrowContactEmailAddressException(string invalidEmailAddress)
        {
            // Arrange
            AddContactInput addContactInput = new()
            {            
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = invalidEmailAddress,
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "12",
            };
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            Mock<IRegisteredContactChecker> registeredContactChecker = new();
            Mock<IContactRepository> contactRepository = new();
            AddNewContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object, registeredContactChecker.Object);

            // Assert
            ContactEmailAddressException exception = await Assert.ThrowsAsync<ContactEmailAddressException>(() => handler.Handle(addContactInput, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.EmailAddressValue.Should().Be(invalidEmailAddress);          
        }

        [Theory(DisplayName = "Register a contact with their phone number in an invalid format")]
        [InlineData("0123456789")]
        [InlineData("1122334455")]
        [InlineData("9876543200")]
        [InlineData("1111111111")]
        [InlineData("123456789012")]
        [InlineData("87654321A")]
        [InlineData("8#7654321")]
        [InlineData("(123)456-7890")]
        [InlineData("987.654.3210")]
        [InlineData("8765432@10")]
        [InlineData("")]
        [InlineData(" ")]
        [Trait("Action", "Handle")]
        public async Task Handle_RegisterContactWithPhoneNumberInAnInvalidFormat_ShouldThrowContactPhoneNumberException(string invalidPhoneNumber)
        {
            // Arrange
            AddContactInput addContactInput = new()
            {
                
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = invalidPhoneNumber,
                ContactPhoneNumberAreaCode = "12",
            };
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            contactPhoneFactory
                .Setup(c => c.CreateAsync(addContactInput.ContactPhoneNumber, addContactInput.ContactPhoneNumberAreaCode))
                .ThrowsAsync(new ContactPhoneNumberException("Phone number is in an invalid format", addContactInput.ContactPhoneNumber));
            Mock<IRegisteredContactChecker> registeredContactChecker = new();
            Mock<IContactRepository> contactRepository = new();
            AddNewContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object, registeredContactChecker.Object);

            // Assert
            ContactPhoneNumberException exception = await Assert.ThrowsAsync<ContactPhoneNumberException>(() => handler.Handle(addContactInput, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.PhoneNumber.Should().Be(invalidPhoneNumber);         
        }

        [Fact(DisplayName = "Registering an existing contact")]
        [Trait("Action", "Handle")]
        public async Task Handle_ContactAlreadyRegistered_ShouldThrowDomainException()
        {
            // Arrange
            AddContactInput addContactInput = new()
            {

                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "11",
            };
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            contactPhoneFactory
                .Setup(c => c.CreateAsync(addContactInput.ContactPhoneNumber, addContactInput.ContactPhoneNumberAreaCode))
                .ReturnsAsync(new ContactPhoneValueObject(addContactInput.ContactPhoneNumber, AreaCodeValueObject.Create(addContactInput.ContactPhoneNumberAreaCode)));
            Mock<IRegisteredContactChecker> registeredContactChecker = new();
            registeredContactChecker.Setup(r => r.CheckRegisteredContactAsync(It.Is<ContactEntity>(contact => contact != null))).ReturnsAsync(true);
            Mock<IContactRepository> contactRepository = new();
            AddNewContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object, registeredContactChecker.Object);

            // Assert
            DomainException exception = await Assert.ThrowsAsync<DomainException>(() => handler.Handle(addContactInput, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
        }
    }
}