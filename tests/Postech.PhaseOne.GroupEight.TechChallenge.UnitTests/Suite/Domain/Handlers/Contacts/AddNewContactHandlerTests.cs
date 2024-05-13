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
    public class AddNewContactHandlerTests
    {
        private readonly Faker _faker = new("pt_BR");

        [Fact(DisplayName = "Inserting a registered contact")]
        [Trait("Action", "Handle")]
        public async Task Handle_InsertingContact_ShouldInsertContact()
        {
            // Arrange
            ContactNameValueObject registeredContactName = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject registeredContactEmail = new(_faker.Internet.Email());
            ContactPhoneValueObject registeredContactPhone = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("11"));
            ContactEntity registeredContact = new(registeredContactName, registeredContactEmail, registeredContactPhone);
            ContactInput input = new()
            {
                
                Name = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                Email = _faker.Internet.Email(),
                Phone = _faker.Phone.PhoneNumber("9########"),
                AreaCode = "12",
            };
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.InsertAsync(registeredContact));
            contactRepository.Setup(c => c.SaveChangesAsync());
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            contactPhoneFactory
                .Setup(c => c.CreateAsync(input.Phone, input.AreaCode))
                .ReturnsAsync(new ContactPhoneValueObject(input.Phone, 
                    AreaCodeValueObject.Create(input.AreaCode)));
            AddNewContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object);         
            
            // Act
            DefaultOutput output = await handler.Handle(input, CancellationToken.None);

            // Assert
            output.Success.Should().BeTrue();
            output.Message.Should().NotBeNullOrEmpty();
          
        }

        
        [Theory(DisplayName = "Insert a registered contact with a new first name in an invalid format")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("B54no")]
        [InlineData("L!c?s")]
        [InlineData("Ta*tiana")]
        [Trait("Action", "Handle")]
        public async Task Handle_InsertRegisteredContactWithNewFirstNameInAnInvalidFormat_ShouldThrowContactFirstNameException(string newInvalidFirstName)
        {
            // Arrange
            ContactNameValueObject registeredContactName = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject registeredContactEmail = new(_faker.Internet.Email());
            ContactPhoneValueObject registeredContactPhone = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("11"));
            ContactEntity registeredContact = new(registeredContactName, registeredContactEmail, registeredContactPhone);
            ContactInput input = new()
            {
                
                Name = newInvalidFirstName,
                LastName = _faker.Name.LastName(),
                Email = _faker.Internet.Email(),
                Phone = _faker.Phone.PhoneNumber("9########"),
                AreaCode = "12",
            };
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.InsertAsync(registeredContact));
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            AddNewContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object);

            // Assert
            ContactFirstNameException exception = await Assert.ThrowsAsync<ContactFirstNameException>(() => handler.Handle(input, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.FirstNameValue.Should().Be(newInvalidFirstName);            
        }

        [Theory(DisplayName = "Inserting a contact with a new last name in an invalid format")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("Jh!ffe?son")]
        [InlineData("[ontarroy@s")]
        [InlineData( "Lim,")]
        [InlineData("Silva ")]
        [InlineData("Alves Gom^s")]
        [Trait("Action", "Handle")]
        public async Task Handle_InsertingContactWithNewLastNameInAnInvalidFormat_ShouldThrowContactLastNameException(string newInvalidLastName)
        {
            // Arrange
            ContactNameValueObject registeredContactName = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject registeredContactEmail = new(_faker.Internet.Email());
            ContactPhoneValueObject registeredContactPhone = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("11"));
            ContactEntity registeredContact = new(registeredContactName, registeredContactEmail, registeredContactPhone);
            ContactInput input = new()
            {            
                Name = _faker.Name.FirstName(),
                LastName = newInvalidLastName,
                Email = _faker.Internet.Email(),
                Phone = _faker.Phone.PhoneNumber("9########"),
                AreaCode = "12",
            };
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.InsertAsync(registeredContact));
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            AddNewContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object);

            // Assert
            ContactLastNameException exception = await Assert.ThrowsAsync<ContactLastNameException>(() => handler.Handle(input, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.LastNameValue.Should().Be(newInvalidLastName);
            
        }

        [Theory(DisplayName = "Insert a registered contact with a new email address in an invalid format")]
        [InlineData("cleiton dias@gmail.com")]
        [InlineData("jair.raposo@")]
        [InlineData("milton.morgado4@hotmail")]
        [InlineData("leticia-mariagmail.com")]
        [InlineData("@yahoo.com")]
        [InlineData("")]
        [InlineData(" ")]
        [Trait("Action", "Handle")]
        public async Task Handle_InsertingContactWithEmailAddressInAnInvalidFormat_ShouldThrowContactEmailAddressException(string newInvalidEmailAddress)
        {
            // Arrange
            ContactNameValueObject registeredContactName = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject registeredContactEmail = new(_faker.Internet.Email());
            ContactPhoneValueObject registeredContactPhone = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("11"));
            ContactEntity registeredContact = new(registeredContactName, registeredContactEmail, registeredContactPhone);
            ContactInput input = new()
            {            
                Name = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                Email = newInvalidEmailAddress,
                Phone = _faker.Phone.PhoneNumber("9########"),
                AreaCode = "12",
            };
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.InsertAsync(registeredContact));
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            AddNewContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object);

            // Assert
            ContactEmailAddressException exception = await Assert.ThrowsAsync<ContactEmailAddressException>(() => handler.Handle(input, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.EmailAddressValue.Should().Be(newInvalidEmailAddress);
            
        }

        [Theory(DisplayName = "Inserting contact with a phone number in an invalid format")]
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
        public async Task Handle_InsertingContactWithNewPhoneNumberInAnInvalidFormat_ShouldThrowContactPhoneNumberException(string newInvalidPhoneNumber)
        {
            // Arrange
            ContactNameValueObject registeredContactName = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject registeredContactEmail = new(_faker.Internet.Email());
            ContactPhoneValueObject registeredContactPhone = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("11"));
            ContactEntity registeredContact = new(registeredContactName, registeredContactEmail, registeredContactPhone);
            ContactInput input = new()
            {
                
                Name = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                Email = _faker.Internet.Email(),
                Phone = newInvalidPhoneNumber,
                AreaCode = "12",
            };
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.InsertAsync(registeredContact));
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            contactPhoneFactory
                .Setup(c => c.CreateAsync(input.Phone, input.AreaCode))
                .ThrowsAsync(new ContactPhoneNumberException("The phone number is invalid.", newInvalidPhoneNumber));
            AddNewContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object);

            // Assert
            ContactPhoneNumberException exception = await Assert.ThrowsAsync<ContactPhoneNumberException>(() => handler.Handle(input, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.PhoneNumber.Should().Be(newInvalidPhoneNumber);
            
        }

        [Theory(DisplayName = "Inserting a registered contact with a new non existing area code")]
        [InlineData("01")]
        [InlineData("90")]
        [InlineData("100")]
        [InlineData("1000")]
        [Trait("Action", "Handle")]
        public async Task Handle_InsertingContactWithNonExistingAreaCode_ShouldThrowNotFoundException(string nonExistingAreaCode)
        {
            // Arrange
            ContactNameValueObject registeredContactName = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject registeredContactEmail = new(_faker.Internet.Email());
            ContactPhoneValueObject registeredContactPhone = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("11"));
            ContactEntity registeredContact = new(registeredContactName, registeredContactEmail, registeredContactPhone);
            ContactInput input = new()
            {            
                Name = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                Email = _faker.Internet.Email(),
                Phone = _faker.Phone.PhoneNumber("9########"),
                AreaCode = nonExistingAreaCode,
            };
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.GetByIdAsync(registeredContact.Id)).ReturnsAsync(registeredContact);
            Mock<IContactPhoneValueObjectFactory> contactPhoneFactory = new();
            contactPhoneFactory
                .Setup(c => c.CreateAsync(input.Phone, input.AreaCode))
                .ThrowsAsync(new NotFoundException("The area code was not found."));
            AddNewContactHandler handler = new(contactRepository.Object, contactPhoneFactory.Object);

            // Assert
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(input, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
            
        }
    }
}