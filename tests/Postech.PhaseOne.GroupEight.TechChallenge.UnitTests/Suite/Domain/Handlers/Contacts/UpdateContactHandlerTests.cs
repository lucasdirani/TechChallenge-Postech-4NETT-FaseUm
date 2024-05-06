using Bogus;
using FluentAssertions;
using Moq;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects;
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
            // Arrange
            ContactNameValueObject registeredContactName = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject registeredContactEmail = new(_faker.Internet.Email());
            ContactPhoneValueObject registeredContactPhone = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("11"));
            ContactEntity registeredContact = new(registeredContactName, registeredContactEmail, registeredContactPhone);
            UpdateContactInput input = new()
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
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            contactPhoneFactory
                .Setup(c => c.CreateAsync(input.ContactPhoneNumber, input.ContactPhoneNumberAreaCode))
                .ReturnsAsync(new ContactPhoneValueObject(input.ContactPhoneNumber, AreaCodeValueObject.Create(input.ContactPhoneNumberAreaCode)));
            UpdateContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object);         
            
            // Act
            DefaultOutput output = await handler.Handle(input, CancellationToken.None);

            // Assert
            output.Success.Should().BeTrue();
            output.Message.Should().NotBeNullOrEmpty();
            registeredContact.ContactName.FirstName.Should().Be(input.ContactFirstName);
            registeredContact.ContactName.LastName.Should().Be(input.ContactLastName);
            registeredContact.ContactEmail.Value.Should().Be(input.ContactEmail); 
            registeredContact.ContactPhone.Number.Should().Be(input.ContactPhoneNumber); 
            registeredContact.ContactPhone.AreaCode.Value.Should().Be(input.ContactPhoneNumberAreaCode); 
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
            UpdateContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object);
            UpdateContactInput input = new()
            {
                ContactId = contactIdThatWillBeUpdated,
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "11"
            };

            // Assert
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(input, CancellationToken.None));
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
            UpdateContactInput input = new()
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
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            UpdateContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object);

            // Assert
            ContactFirstNameException exception = await Assert.ThrowsAsync<ContactFirstNameException>(() => handler.Handle(input, CancellationToken.None));
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
            UpdateContactInput input = new()
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
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            UpdateContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object);

            // Assert
            ContactLastNameException exception = await Assert.ThrowsAsync<ContactLastNameException>(() => handler.Handle(input, CancellationToken.None));
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
            UpdateContactInput input = new()
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
            UpdateContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object);

            // Assert
            ContactEmailAddressException exception = await Assert.ThrowsAsync<ContactEmailAddressException>(() => handler.Handle(input, CancellationToken.None));
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
            UpdateContactInput input = new()
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
            contactPhoneFactory
                .Setup(c => c.CreateAsync(input.ContactPhoneNumber, input.ContactPhoneNumberAreaCode))
                .ThrowsAsync(new ContactPhoneNumberException("The phone number is invalid.", newInvalidPhoneNumber));
            UpdateContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object);

            // Assert
            ContactPhoneNumberException exception = await Assert.ThrowsAsync<ContactPhoneNumberException>(() => handler.Handle(input, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.PhoneNumber.Should().Be(newInvalidPhoneNumber);
            contactRepository.Verify(c => c.GetByIdAsync(registeredContact.Id), Times.Once());
            contactRepository.Verify(c => c.Update(registeredContact), Times.Never());
            contactRepository.Verify(c => c.SaveChangesAsync(), Times.Never());
        }

        [Theory(DisplayName = "Updating a registered contact with a new non existing area code")]
        [InlineData("01")]
        [InlineData("90")]
        [InlineData("100")]
        [InlineData("1000")]
        [Trait("Action", "Handle")]
        public async Task Handle_UpdatingRegisteredContactWithNewNonExistingAreaCode_ShouldThrowNotFoundException(string nonExistingAreaCode)
        {
            // Arrange
            ContactNameValueObject registeredContactName = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject registeredContactEmail = new(_faker.Internet.Email());
            ContactPhoneValueObject registeredContactPhone = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("11"));
            ContactEntity registeredContact = new(registeredContactName, registeredContactEmail, registeredContactPhone);
            UpdateContactInput input = new()
            {
                ContactId = registeredContact.Id,
                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = nonExistingAreaCode,
            };
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.GetByIdAsync(registeredContact.Id)).ReturnsAsync(registeredContact);
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            contactPhoneFactory
                .Setup(c => c.CreateAsync(input.ContactPhoneNumber, input.ContactPhoneNumberAreaCode))
                .ThrowsAsync(new NotFoundException("The area code was not found."));
            UpdateContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object);

            // Assert
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(input, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
            contactRepository.Verify(c => c.GetByIdAsync(registeredContact.Id), Times.Once());
            contactRepository.Verify(c => c.Update(registeredContact), Times.Never());
            contactRepository.Verify(c => c.SaveChangesAsync(), Times.Never());
        }
    }
}