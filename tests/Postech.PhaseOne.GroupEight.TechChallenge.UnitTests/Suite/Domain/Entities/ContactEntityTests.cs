using Bogus;
using FluentAssertions;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.UnitTests.Suite.Domain.Entities
{
    public class ContactEntityTests
    {
        private readonly Faker _faker = new("pt_BR");

        [Theory(DisplayName = "Constructing a valid object of type ContactEntity")]
        [InlineData("Lucas", "Dirani", "lucas.dirani@gmail.com", "11", "987654321")]
        [InlineData("Ricardo", "Fulgencio", "rfulgencio3@gmail.com", "11", "876543210")]
        [InlineData("Breno", "Jhefferson", "breno.jgomes2@gmail.com", "11", "234567890")]
        [InlineData("Lucas", "Montarroyos", "lucaspinho101@gmail.com", "11", "765432109")]
        [InlineData("Tatiana", "Lima", "tatidornel@gmail.com", "11", "890123456")]
        [Trait("Action", "ContactEntity")]
        public void ContactEntity_ValidData_ShouldConstructContactEntity(
            string contactFirstName,
            string contactLastName,
            string contactEmailAddress,
            string areaCodePhoneNumber,
            string contactPhoneNumber)
        {
            // Arrange
            ContactNameValueObject contactName = new(contactFirstName, contactLastName);
            ContactEmailValueObject contactEmail = new(contactEmailAddress);
            ContactPhoneValueObject contactPhone = new(contactPhoneNumber, AreaCodeValueObject.Create(areaCodePhoneNumber));
            
            // Act
            ContactEntity contact = new(contactName, contactEmail, contactPhone);

            // Assert
            contact.Should().NotBeNull();
            contact.Id.Should().NotBeEmpty();
            contact.IsActive().Should().BeTrue();
            contact.CreatedAt.Should().BeOnOrBefore(DateTime.UtcNow);
            contact.ModifiedAt.Should().BeOnOrBefore(DateTime.UtcNow);
            contact.ContactName.Should().Be(contactName);
            contact.ContactEmail.Should().Be(contactEmail);
            contact.ContactPhone.Should().Be(contactPhone);
        }

        [Theory(DisplayName = "Updating the contact's first name")]
        [InlineData("Lucas", "Lucca")]
        [InlineData("Cristiane", "Cristina")]
        [InlineData("Rafael", "Raphael")]
        [Trait("Action", "UpdateContactName")]
        public void UpdateContactName_UpdatingContactFirstName_ShouldUpdateContactName(string contactCurrentFirstName, string contactNewFirstName)
        {
            // Arrange
            ContactNameValueObject contactName = new(contactCurrentFirstName, _faker.Name.LastName());
            ContactEmailValueObject contactEmail = new(_faker.Internet.Email());
            ContactPhoneValueObject contactPhone = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("11"));
            ContactEntity contact = new(contactName, contactEmail, contactPhone);

            // Act
            contact.UpdateContactName(contactNewFirstName, contactName.LastName);

            // Assert
            contact.ContactName.Should().NotBe(contactName);
            contact.ContactName.FirstName.Should().Be(contactNewFirstName);
        }

        [Theory(DisplayName = "Updating the contact's last name")]
        [InlineData("Ruiz", "Dirani")]
        [InlineData("Silva", "Lima")]
        [InlineData("Souza", "Santos")]
        [Trait("Action", "UpdateContactName")]
        public void UpdateContactName_UpdatingContactLastName_ShouldUpdateContactName(string contactCurrentLastName, string contactNewLastName)
        {
            // Arrange
            ContactNameValueObject contactName = new(_faker.Name.FirstName(), contactCurrentLastName);
            ContactEmailValueObject contactEmail = new(_faker.Internet.Email());
            ContactPhoneValueObject contactPhone = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("11"));
            ContactEntity contact = new(contactName, contactEmail, contactPhone);

            // Act
            contact.UpdateContactName(contactName.FirstName, contactNewLastName);

            // Assert
            contact.ContactName.Should().NotBe(contactName);
            contact.ContactName.LastName.Should().Be(contactNewLastName);
        }

        [Theory(DisplayName = "Updating the contact's first name and last name")]
        [InlineData("Lucas", "Dirani", "Lucca", "Ruiz")]
        [InlineData("Cristiane", "Jesus", "Cristina", "Andrade")]
        [InlineData("Rafael", "Silva", "Raphael", "Souza")]
        [Trait("Action", "UpdateContactName")]
        public void UpdateContactName_UpdatingContactFirstNameAndLastName_ShouldUpdateContactName(
            string contactCurrentFirstName,
            string contactCurrentLastName, 
            string contactNewFirstName,
            string contactNewLastName)
        {
            // Arrange
            ContactNameValueObject contactName = new(contactCurrentFirstName, contactCurrentLastName);
            ContactEmailValueObject contactEmail = new(_faker.Internet.Email());
            ContactPhoneValueObject contactPhone = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("11"));
            ContactEntity contact = new(contactName, contactEmail, contactPhone);

            // Act
            contact.UpdateContactName(contactNewFirstName, contactNewLastName);

            // Assert
            contact.ContactName.Should().NotBe(contactName);
            contact.ContactName.FirstName.Should().Be(contactNewFirstName);
            contact.ContactName.LastName.Should().Be(contactNewLastName);
        }

        [Theory(DisplayName = "Updating the contact's email address")]
        [InlineData("lucas.dirani@gmail.com", "lucas.ruiz@gmail.com")]
        [InlineData("cristiane.silva@hotmail.com", "cristiane.silva@gmail.com")]
        [Trait("Action", "UpdateContactEmail")]
        public void UpdateContactEmail_UpdatingContactEmailAddress_ShouldUpdateContactEmail(string contactCurrentEmailAddress, string contactNewEmailAddress)
        {
            // Arrange
            ContactNameValueObject contactName = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject contactEmail = new(contactCurrentEmailAddress);
            ContactPhoneValueObject contactPhone = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("11"));
            ContactEntity contact = new(contactName, contactEmail, contactPhone);

            // Act
            contact.UpdateContactEmail(contactNewEmailAddress);

            // Assert
            contact.ContactEmail.Should().NotBe(contactEmail);
            contact.ContactEmail.Value.Should().Be(contactNewEmailAddress);
        }

        [Theory(DisplayName = "Updating the contact's phone number")]
        [InlineData("987654325", "987654330")]
        [InlineData("987654363", "987654370")]
        [Trait("Action", "UpdateContactPhone")]
        public void UpdateContactPhone_UpdatingContactPhoneNumber_ShouldUpdateContactPhone(string contactCurrentPhoneNumber, string contactNewPhoneNumber)
        {
            // Arrange
            ContactNameValueObject contactName = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject contactEmail = new(_faker.Internet.Email());
            ContactPhoneValueObject contactCurrentPhone = new(contactCurrentPhoneNumber, AreaCodeValueObject.Create("11"));
            ContactPhoneValueObject contactNewPhone = new(contactNewPhoneNumber, AreaCodeValueObject.Create("11"));
            ContactEntity contact = new(contactName, contactEmail, contactCurrentPhone);

            // Act
            contact.UpdateContactPhone(contactNewPhone);

            // Assert
            contact.ContactPhone.Should().NotBe(contactCurrentPhone);
            contact.ContactPhone.Should().Be(contactNewPhone);
        }

        [Theory(DisplayName = "Updating the contact's area code phone number")]
        [InlineData("11", "13")]
        [InlineData("21", "22")]
        [Trait("Action", "UpdateContactPhone")]
        public void UpdateContactPhone_UpdatingContactAreaCodePhoneNumber_ShouldUpdateContactPhone(string contactCurrentAreaCodePhoneNumber, string contactNewAreaCodePhoneNumber)
        {
            // Arrange
            ContactNameValueObject contactName = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject contactEmail = new(_faker.Internet.Email());
            ContactPhoneValueObject contactCurrentPhone = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create(contactCurrentAreaCodePhoneNumber));
            ContactPhoneValueObject contactNewPhone = new(contactCurrentPhone.Number, AreaCodeValueObject.Create(contactNewAreaCodePhoneNumber));
            ContactEntity contact = new(contactName, contactEmail, contactCurrentPhone);

            // Act
            contact.UpdateContactPhone(contactNewPhone);

            // Assert
            contact.ContactPhone.Should().NotBe(contactCurrentPhone);
            contact.ContactPhone.Should().Be(contactNewPhone);
        }

        [Theory(DisplayName = "Updating the contact's area code phone number")]
        [InlineData("987654325", "11", "987664355", "13")]
        [InlineData("987654325", "21", "987664355", "22")]
        [Trait("Action", "UpdateContactPhone")]
        public void UpdateContactPhone_UpdatingContactPhoneNumberAndAreaCodePhoneNumber_ShouldUpdateContactPhone(
            string contactCurrentPhoneNumber,
            string contactCurrentAreaCodePhoneNumber, 
            string contactNewPhoneNumber,
            string contactNewAreaCodePhoneNumber)
        {
            // Arrange
            ContactNameValueObject contactName = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject contactEmail = new(_faker.Internet.Email());
            ContactPhoneValueObject contactCurrentPhone = new(contactCurrentPhoneNumber, AreaCodeValueObject.Create(contactCurrentAreaCodePhoneNumber));
            ContactPhoneValueObject contactNewPhone = new(contactNewPhoneNumber, AreaCodeValueObject.Create(contactNewAreaCodePhoneNumber));
            ContactEntity contact = new(contactName, contactEmail, contactCurrentPhone);

            // Act
            contact.UpdateContactPhone(contactNewPhone);

            // Assert
            contact.ContactPhone.Should().NotBe(contactCurrentPhone);
            contact.ContactPhone.Should().Be(contactNewPhone);
        }
    }
}