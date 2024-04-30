using FluentAssertions;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.UnitTests.Suite.Domain.ValueObjects
{
    public class ContactPhoneValueObjectTests
    {
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
    }
}