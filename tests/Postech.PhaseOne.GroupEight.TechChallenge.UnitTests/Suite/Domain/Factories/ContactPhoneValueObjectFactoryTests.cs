using FluentAssertions;
using Moq;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Factories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.UnitTests.Suite.Domain.Factories
{
    public class ContactPhoneValueObjectFactoryTests
    {
        [Theory(DisplayName = "Creating a new valid phone number")]
        [InlineData("98765432", "11")]
        [InlineData("87654321", "21")]
        [InlineData("76543210", "31")]
        [InlineData("998765432", "11")]
        [InlineData("987654321", "21")]
        [InlineData("976543210", "31")]
        [Trait("Action", "CreateAsync")]
        public async Task CreateAsync_NewValidPhoneNumber_ShouldCreatePhoneNumber(string phoneNumber, string areaCodeValue)
        {
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.GetAreaCodeByValueAsync(areaCodeValue)).ReturnsAsync(AreaCodeValueObject.Create(areaCodeValue));
            ContactPhoneValueObjectFactory factory = new(contactRepository.Object);
            ContactPhoneValueObject contactPhone = await factory.CreateAsync(phoneNumber, areaCodeValue);
            contactPhone.Number.Should().Be(phoneNumber);
            contactPhone.AreaCode.Value.Should().Be(areaCodeValue);
        }

        [Theory(DisplayName = "Creating a phone number already registered in the database")]
        [InlineData("89012345", "11")]
        [InlineData("76541098", "21")]
        [InlineData("67890123", "31")]
        [InlineData("765432109", "11")]
        [InlineData("876543210", "21")]
        [InlineData("987654321", "31")]
        [Trait("Action", "CreateAsync")]
        public async Task CreateAsync_PhoneNumberAlreadyRegisteredInTheDatabase_ShouldCreatePhoneNumber(string phoneNumber, string areaCodeValue)
        {
            ContactPhoneValueObject contactPhoneRegisteredInTheDatabase = new(phoneNumber, AreaCodeValueObject.Create(areaCodeValue));
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.GetContactPhoneByNumberAndAreaCodeValueAsync(phoneNumber, areaCodeValue)).ReturnsAsync(contactPhoneRegisteredInTheDatabase);
            ContactPhoneValueObjectFactory factory = new(contactRepository.Object);
            ContactPhoneValueObject contactPhone = await factory.CreateAsync(phoneNumber, areaCodeValue);
            contactPhone.Should().Be(contactPhoneRegisteredInTheDatabase);
            contactRepository.Verify(c => c.GetContactPhoneByNumberAndAreaCodeValueAsync(phoneNumber, areaCodeValue), Times.Once());
            contactRepository.Verify(c => c.GetAreaCodeByValueAsync(areaCodeValue), Times.Never());
        }

        [Theory(DisplayName = "Creating a phone number with an invalid area code")]
        [InlineData("89012345", "01")]
        [InlineData("76541098", "23")]
        [InlineData("67890123", "36")]
        [InlineData("765432109", "95")]
        [InlineData("876543210", "100")]
        [InlineData("987654321", "87")]
        [Trait("Action", "CreateAsync")]
        public async Task CreateAsync_PhoneNumberWithAnInvalidAreaCode_ShouldThrowNotFoundException(string phoneNumber, string areaCodeValue)
        {
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.GetAreaCodeByValueAsync(areaCodeValue)).ReturnsAsync(() => null);
            ContactPhoneValueObjectFactory factory = new(contactRepository.Object);
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(() => factory.CreateAsync(phoneNumber, areaCodeValue));
            exception.Message.Should().NotBeNullOrEmpty();
        }

        [Theory(DisplayName = "Creating a new invalid phone number")]
        [InlineData("12345678", "11")]
        [InlineData("9876543", "21")]
        [InlineData("7654321098", "31")]
        [InlineData("123456789", "11")]
        [Trait("Action", "CreateAsync")]
        public async Task CreateAsync_NewInvalidPhoneNumber_ShouldThrowContactPhoneNumberException(string phoneNumber, string areaCodeValue)
        {
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.GetAreaCodeByValueAsync(areaCodeValue)).ReturnsAsync(AreaCodeValueObject.Create(areaCodeValue));
            ContactPhoneValueObjectFactory factory = new(contactRepository.Object);
            ContactPhoneNumberException exception = await Assert.ThrowsAsync<ContactPhoneNumberException>(() => factory.CreateAsync(phoneNumber, areaCodeValue));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.PhoneNumber.Should().Be(phoneNumber);
        }
    }
}