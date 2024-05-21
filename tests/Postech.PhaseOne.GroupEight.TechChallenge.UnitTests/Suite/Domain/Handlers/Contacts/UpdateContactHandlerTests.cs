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
    public class UpdateContactHandlerTests 
    {
        private readonly Faker _faker = new("pt_BR");

        [Fact(DisplayName = "Updating a registered contact")]
        [Trait("Action", "Handle")]
        public async Task Handle_UpdatingRegisteredContact_ShouldUpdateContact()
        {
            // Arrange
            ContactNameValueObject registeredContactName = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject registeredContactEmail = new(_faker.Internet.Email());
            ContactPhoneValueObject registeredContactPhone = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("11"));
            ContactEntity registeredContact = new(registeredContactName, registeredContactEmail, registeredContactPhone);
            UpdateContactInput updateContactInput = new()
            {
                ContactId = registeredContact.Id,
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "12",
            };
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.GetByIdAsync(registeredContact.Id)).ReturnsAsync(registeredContact);
            contactRepository.Setup(c => c.SaveChangesAsync());
            Mock<IRegisteredContactChecker> registeredContactChecker = new();
            registeredContactChecker.Setup(r => r.CheckRegisteredContactAsync(It.Is<ContactEntity>(contact => contact != null))).ReturnsAsync(false);
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            contactPhoneFactory
                .Setup(c => c.CreateAsync(updateContactInput.ContactPhoneNumber, updateContactInput.ContactPhoneNumberAreaCode))
                .ReturnsAsync(new ContactPhoneValueObject(updateContactInput.ContactPhoneNumber, AreaCodeValueObject.Create(updateContactInput.ContactPhoneNumberAreaCode)));
            UpdateContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object, registeredContactChecker.Object);         
            
            // Act
            DefaultOutput<UpdateContactViewModel> output = await handler.Handle(updateContactInput, CancellationToken.None);

            // Assert
            output.Success.Should().BeTrue();
            output.Message.Should().NotBeNullOrEmpty();
            registeredContact.ContactName.FirstName.Should().Be(updateContactInput.ContactFirstName);
            registeredContact.ContactName.LastName.Should().Be(updateContactInput.ContactLastName);
            registeredContact.ContactEmail.Value.Should().Be(updateContactInput.ContactEmail); 
            registeredContact.ContactPhone.Number.Should().Be(updateContactInput.ContactPhoneNumber); 
            registeredContact.ContactPhone.AreaCode.Value.Should().Be(updateContactInput.ContactPhoneNumberAreaCode); 
            registeredContact.ModifiedAt.Should().BeOnOrBefore(DateTime.UtcNow);
            contactRepository.Verify(c => c.GetByIdAsync(registeredContact.Id), Times.Once());
            contactRepository.Verify(c => c.Update(registeredContact), Times.Once());
            contactRepository.Verify(c => c.SaveChangesAsync(), Times.Once());
        }

        [Fact(DisplayName = "Updating an unregistered contact")]
        [Trait("Action", "Handle")]
        public async Task Handle_UpdatingUnregisteredContact_ShouldThrowNotFoundException()
        {
            // Arrange
            Guid contactIdThatWillBeUpdated = _faker.Random.Guid();
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.GetByIdAsync(contactIdThatWillBeUpdated)).ReturnsAsync(() => null);
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            Mock<IRegisteredContactChecker> registeredContactChecker = new();
            UpdateContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object, registeredContactChecker.Object);
            UpdateContactInput updateContactInput = new()
            {
                ContactId = contactIdThatWillBeUpdated,
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "11"
            };

            // Assert
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(updateContactInput, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
            contactRepository.Verify(c => c.GetByIdAsync(contactIdThatWillBeUpdated), Times.Once());
            contactRepository.Verify(c => c.Update(It.Is<ContactEntity>(entity => entity != null)), Times.Never());
            contactRepository.Verify(c => c.SaveChangesAsync(), Times.Never());
        }

        [Theory(DisplayName = "Updating a registered contact with a new first name in an invalid format")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("B54no")]
        [InlineData("L!c?s")]
        [InlineData("Ta*tiana")]
        [Trait("Action", "Handle")]
        public async Task Handle_UpdatingRegisteredContactWithNewFirstNameInAnInvalidFormat_ShouldThrowContactFirstNameException(string newInvalidFirstName)
        {
            // Arrange
            ContactNameValueObject registeredContactName = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject registeredContactEmail = new(_faker.Internet.Email());
            ContactPhoneValueObject registeredContactPhone = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("11"));
            ContactEntity registeredContact = new(registeredContactName, registeredContactEmail, registeredContactPhone);
            UpdateContactInput updateContactInput = new()
            {
                ContactId = registeredContact.Id,
                ContactFirstName = newInvalidFirstName,
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "12",
            };
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.GetByIdAsync(registeredContact.Id)).ReturnsAsync(registeredContact);
            Mock<IRegisteredContactChecker> registeredContactChecker = new();
            registeredContactChecker.Setup(r => r.CheckRegisteredContactAsync(It.Is<ContactEntity>(contact => contact != null))).ReturnsAsync(false);
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            UpdateContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object, registeredContactChecker.Object);

            // Assert
            ContactFirstNameException exception = await Assert.ThrowsAsync<ContactFirstNameException>(() => handler.Handle(updateContactInput, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.FirstNameValue.Should().Be(newInvalidFirstName);
            contactRepository.Verify(c => c.GetByIdAsync(registeredContact.Id), Times.Once());
            contactRepository.Verify(c => c.Update(registeredContact), Times.Never());
            contactRepository.Verify(c => c.SaveChangesAsync(), Times.Never());
        }

        [Theory(DisplayName = "Updating a registered contact with a new last name in an invalid format")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("Jh!ffe?son")]
        [InlineData("[ontarroy@s")]
        [InlineData( "Lim,")]
        [InlineData("Silva ")]
        [InlineData("Alves Gom^s")]
        [Trait("Action", "Handle")]
        public async Task Handle_UpdatingRegisteredContactWithNewLastNameInAnInvalidFormat_ShouldThrowContactLastNameException(string newInvalidLastName)
        {
            // Arrange
            ContactNameValueObject registeredContactName = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject registeredContactEmail = new(_faker.Internet.Email());
            ContactPhoneValueObject registeredContactPhone = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("11"));
            ContactEntity registeredContact = new(registeredContactName, registeredContactEmail, registeredContactPhone);
            UpdateContactInput updateContactInput = new()
            {
                ContactId = registeredContact.Id,
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = newInvalidLastName,
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "12",
            };
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.GetByIdAsync(registeredContact.Id)).ReturnsAsync(registeredContact);
            Mock<IRegisteredContactChecker> registeredContactChecker = new();
            registeredContactChecker.Setup(r => r.CheckRegisteredContactAsync(It.Is<ContactEntity>(contact => contact != null))).ReturnsAsync(false);
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            UpdateContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object, registeredContactChecker.Object);

            // Assert
            ContactLastNameException exception = await Assert.ThrowsAsync<ContactLastNameException>(() => handler.Handle(updateContactInput, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.LastNameValue.Should().Be(newInvalidLastName);
            contactRepository.Verify(c => c.GetByIdAsync(registeredContact.Id), Times.Once());
            contactRepository.Verify(c => c.Update(registeredContact), Times.Never());
            contactRepository.Verify(c => c.SaveChangesAsync(), Times.Never());
        }

        [Theory(DisplayName = "Updating a registered contact with a new email address in an invalid format")]
        [InlineData("cleiton dias@gmail.com")]
        [InlineData("jair.raposo@")]
        [InlineData("milton.morgado4@hotmail")]
        [InlineData("leticia-mariagmail.com")]
        [InlineData("@yahoo.com")]
        [InlineData("")]
        [InlineData(" ")]
        [Trait("Action", "Handle")]
        public async Task Handle_UpdatingRegisteredContactWithNewEmailAddressInAnInvalidFormat_ShouldThrowContactEmailAddressException(string newInvalidEmailAddress)
        {
            // Arrange
            ContactNameValueObject registeredContactName = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject registeredContactEmail = new(_faker.Internet.Email());
            ContactPhoneValueObject registeredContactPhone = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("11"));
            ContactEntity registeredContact = new(registeredContactName, registeredContactEmail, registeredContactPhone);
            UpdateContactInput updateContactInput = new()
            {
                ContactId = registeredContact.Id,
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = newInvalidEmailAddress,
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "12",
            };
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.GetByIdAsync(registeredContact.Id)).ReturnsAsync(registeredContact);
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            Mock<IRegisteredContactChecker> registeredContactChecker = new();
            registeredContactChecker.Setup(r => r.CheckRegisteredContactAsync(It.Is<ContactEntity>(contact => contact != null))).ReturnsAsync(false);
            UpdateContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object, registeredContactChecker.Object);

            // Assert
            ContactEmailAddressException exception = await Assert.ThrowsAsync<ContactEmailAddressException>(() => handler.Handle(updateContactInput, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.EmailAddressValue.Should().Be(newInvalidEmailAddress);
            contactRepository.Verify(c => c.GetByIdAsync(registeredContact.Id), Times.Once());
            contactRepository.Verify(c => c.Update(registeredContact), Times.Never());
            contactRepository.Verify(c => c.SaveChangesAsync(), Times.Never());
        }

        [Theory(DisplayName = "Updating a registered contact with a new phone number in an invalid format")]
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
        public async Task Handle_UpdatingRegisteredContactWithNewPhoneNumberInAnInvalidFormat_ShouldThrowContactPhoneNumberException(string newInvalidPhoneNumber)
        {
            // Arrange
            ContactNameValueObject registeredContactName = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject registeredContactEmail = new(_faker.Internet.Email());
            ContactPhoneValueObject registeredContactPhone = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("11"));
            ContactEntity registeredContact = new(registeredContactName, registeredContactEmail, registeredContactPhone);
            UpdateContactInput updateContactInput = new()
            {
                ContactId = registeredContact.Id,
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = newInvalidPhoneNumber,
                ContactPhoneNumberAreaCode = "12",
            };
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.GetByIdAsync(registeredContact.Id)).ReturnsAsync(registeredContact);
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            Mock<IRegisteredContactChecker> registeredContactChecker = new();
            registeredContactChecker.Setup(r => r.CheckRegisteredContactAsync(It.Is<ContactEntity>(contact => contact != null))).ReturnsAsync(false);
            contactPhoneFactory
                .Setup(c => c.CreateAsync(updateContactInput.ContactPhoneNumber, updateContactInput.ContactPhoneNumberAreaCode))
                .ThrowsAsync(new ContactPhoneNumberException("The phone number is invalid.", newInvalidPhoneNumber));
            UpdateContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object, registeredContactChecker.Object);

            // Assert
            ContactPhoneNumberException exception = await Assert.ThrowsAsync<ContactPhoneNumberException>(() => handler.Handle(updateContactInput, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.PhoneNumber.Should().Be(newInvalidPhoneNumber);
            contactRepository.Verify(c => c.GetByIdAsync(registeredContact.Id), Times.Once());
            contactRepository.Verify(c => c.Update(registeredContact), Times.Never());
            contactRepository.Verify(c => c.SaveChangesAsync(), Times.Never());
        }

        [Fact(DisplayName = "Updating a registered contact with the same data as another contact")]
        [Trait("Action", "Handle")]
        public async Task Handle_UpdatingRegisteredContactWithTheSameDataAsAnotherContact_ShouldThrowDomainException()
        {
            // Arrange
            ContactNameValueObject registeredContactName = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject registeredContactEmail = new(_faker.Internet.Email());
            ContactPhoneValueObject registeredContactPhone = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("11"));
            ContactEntity registeredContact = new(registeredContactName, registeredContactEmail, registeredContactPhone);
            UpdateContactInput updateContactInput = new()
            {
                ContactId = registeredContact.Id,
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "11",
            };
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.GetByIdAsync(registeredContact.Id)).ReturnsAsync(registeredContact);
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            contactPhoneFactory
                .Setup(c => c.CreateAsync(updateContactInput.ContactPhoneNumber, updateContactInput.ContactPhoneNumberAreaCode))
                .ReturnsAsync(new ContactPhoneValueObject(updateContactInput.ContactPhoneNumber, AreaCodeValueObject.Create(updateContactInput.ContactPhoneNumberAreaCode)));
            Mock<IRegisteredContactChecker> registeredContactChecker = new();
            registeredContactChecker.Setup(r => r.CheckRegisteredContactAsync(It.Is<ContactEntity>(contact => contact != null))).ReturnsAsync(true);
            UpdateContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object, registeredContactChecker.Object);

            // Assert
            DomainException exception = await Assert.ThrowsAsync<DomainException>(() => handler.Handle(updateContactInput, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
            contactRepository.Verify(c => c.GetByIdAsync(registeredContact.Id), Times.Once());
            contactRepository.Verify(c => c.Update(registeredContact), Times.Never());
            contactRepository.Verify(c => c.SaveChangesAsync(), Times.Never());
        }
    }
}