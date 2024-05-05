using Bogus;
using FluentAssertions;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.UnitTests.Suite.Domain.ValueObjects
{
    public class ContactPhoneValueObjectTests
    {
        private readonly Faker _faker = new("pt_BR");

        [Theory(DisplayName = "Constructing a valid object of type ContactPhoneValueObject")]
        [InlineData("987654325")]
        [InlineData("987654330")]
        [InlineData("987654343")]
        [InlineData("987654363")]
        [InlineData("987654366")]
        [InlineData("987654369")]
        [InlineData("987654370")]
        [InlineData("87654337")]
        [InlineData("87654327")]
        [InlineData("87654321")]
        [Trait("Action", "ContactPhoneValueObject")]
        public void ContactPhoneValueObject_ValidData_ShouldConstructContactPhoneValueObject(string phoneNumber)
        {
            AreaCodeValueObject areaCode = AreaCodeValueObject.Create("11");
            ContactPhoneValueObject contactPhone = new(phoneNumber, areaCode);
            contactPhone.Should().NotBeNull();
            contactPhone.Number.Should().Be(phoneNumber);
            contactPhone.AreaCode.Should().Be(areaCode);
        }

        [Theory(DisplayName = "Construct an object of type ContactPhoneValueObject with an invalid phone number")]
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
        [Trait("Action", "ContactPhoneValueObject")]
        public void ContactPhoneValueObject_InvalidPhoneNumber_ShouldThrowContactPhoneNumberException(string phoneNumber)
        {
            AreaCodeValueObject areaCode = AreaCodeValueObject.Create("11");
            ContactPhoneNumberException exception = Assert.Throws<ContactPhoneNumberException>(() => new ContactPhoneValueObject(phoneNumber, areaCode));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.PhoneNumber.Should().Be(phoneNumber);
        }

        [Fact(DisplayName = "Construct an object of type ContactPhoneValueObject with an invalid area code")]
        [Trait("Action", "ContactPhoneValueObject")]
        public void ContactPhoneValueObject_InvalidAreaCode_ShouldThrowNotFoundException()
        {
            string phoneNumber = "987654325";
            AreaCodeValueObject? areaCode = null;
            NotFoundException exception = Assert.Throws<NotFoundException>(() => new ContactPhoneValueObject(phoneNumber, areaCode));
            exception.Message.Should().NotBeNullOrEmpty();
        }

        [Theory(DisplayName = "Contact's phone number is being changed.")]
        [InlineData("987654325", "87654321")]
        [InlineData("987654330", "87654327")]
        [Trait("Action", "HasBeenChanged")]
        public void HasBeenChanged_ContactPhoneNumberHasBeenChanged_ShouldReturnTrue(string currentPhoneNumber, string otherPhoneNumber)
        {
            AreaCodeValueObject areaCode = AreaCodeValueObject.Create("11");
            ContactPhoneValueObject contactPhone = new(currentPhoneNumber, areaCode);
            contactPhone.HasBeenChanged(otherPhoneNumber, areaCode).Should().BeTrue();
        }

        [Theory(DisplayName = "Contact's area code phone number is being changed.")]
        [InlineData("11", "13")]
        [InlineData("21", "22")]
        [Trait("Action", "HasBeenChanged")]
        public void HasBeenChanged_ContactAreaCodePhoneNumberHasBeenChanged_ShouldReturnTrue(string currentAreaCode, string otherAreaCode)
        {
            AreaCodeValueObject areaCode = AreaCodeValueObject.Create(currentAreaCode);
            ContactPhoneValueObject contactPhone = new(_faker.Phone.PhoneNumber("9########"), areaCode);
            contactPhone.HasBeenChanged(contactPhone.Number, AreaCodeValueObject.Create(otherAreaCode)).Should().BeTrue();
        }

        [Theory(DisplayName = "Contact's phone number and area code phone number are being changed.")]
        [InlineData("987654325", "11", "87654321", "13")]
        [InlineData("987654330", "21", "87654327", "22")]
        [Trait("Action", "HasBeenChanged")]
        public void HasBeenChanged_ContactPhoneNumberAndAreaCodePhoneNumberHaveBeenChanged_ShouldReturnTrue(
            string currentPhoneNumber,
            string currentAreaCode, 
            string otherPhoneNumber,
            string otherAreaCode)
        {
            AreaCodeValueObject areaCode = AreaCodeValueObject.Create(currentAreaCode);
            ContactPhoneValueObject contactPhone = new(currentPhoneNumber, areaCode);
            contactPhone.HasBeenChanged(otherPhoneNumber, AreaCodeValueObject.Create(otherAreaCode)).Should().BeTrue();
        }

        [Theory(DisplayName = "Contact's phone number and area code phone number are not being changed.")]
        [InlineData("987654325", "11", "987654325", "11")]
        [InlineData("987654330", "21", "987654330", "21")]
        [Trait("Action", "HasBeenChanged")]
        public void HasBeenChanged_ContactPhoneNumberAndAreaCodePhoneNumberHaveNotBeenChanged_ShouldReturnFalse(
            string currentPhoneNumber,
            string currentAreaCode,
            string otherPhoneNumber,
            string otherAreaCode)
        {
            AreaCodeValueObject areaCode = AreaCodeValueObject.Create(currentAreaCode);
            ContactPhoneValueObject contactPhone = new(currentPhoneNumber, areaCode);
            contactPhone.HasBeenChanged(otherPhoneNumber, AreaCodeValueObject.Create(otherAreaCode)).Should().BeFalse();
        }
    }
}