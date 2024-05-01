using FluentAssertions;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.UnitTests.Suite.Domain.Entities
{
    public class ContactEntityTests
    {
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
            ContactNameValueObject contactName = new(contactFirstName, contactLastName);
            ContactEmailValueObject contactEmail = new(contactEmailAddress);
            ContactPhoneValueObject contactPhone = new(contactPhoneNumber, AreaCodeValueObject.Create(areaCodePhoneNumber));
            ContactEntity contact = new(contactName, contactEmail, contactPhone);
            contact.Should().NotBeNull();
            contact.Id.Should().NotBeEmpty();
            contact.IsActive().Should().BeTrue();
            contact.CreatedAt.Should().BeOnOrBefore(DateTime.UtcNow);
            contact.ModifiedAt.Should().BeOnOrBefore(DateTime.UtcNow);
            contact.ContactName.Should().Be(contactName);
            contact.ContactEmail.Should().Be(contactEmail);
            contact.ContactPhone.Should().Be(contactPhone);
        }

        [Theory(DisplayName = "Updating a ContactEntity")]
        [InlineData("Lucas", "Dirani", "lucas.dirani@gmail.com", "11", "987654321")]
        [InlineData("Ricardo", "Fulgencio", "rfulgencio3@gmail.com", "11", "876543210")]
        [InlineData("Breno", "Jhefferson", "breno.jgomes2@gmail.com", "11", "234567890")]
        [InlineData("Lucas", "Montarroyos", "lucaspinho101@gmail.com", "11", "765432109")]
        [InlineData("Tatiana", "Lima", "tatidornel@gmail.com", "11", "890123456")]
        [Trait("Action", "ContactEntity")]
        public void ContactEntity_Update_ShouldUpdateContactEntity(
            string newContactFirstName,
            string newContactLastName,
            string newContactEmailAddress,
            string newAreaCodePhoneNumber,
            string newContactPhoneNumber)
        {
            // Arrange
            ContactNameValueObject contactName = new("John", "Doe");
            ContactEmailValueObject contactEmail = new("johndoe@gmail.com");
            ContactPhoneValueObject contactPhone = new("845123657", AreaCodeValueObject.Create("11"));
            ContactEntity contact = new(contactName, contactEmail, contactPhone);

            // Act
            ContactNameValueObject newContactName = new(newContactFirstName, newContactLastName);
            ContactEmailValueObject newContactEmail = new(newContactEmailAddress);
            ContactPhoneValueObject newContactPhone = new(newContactPhoneNumber, AreaCodeValueObject.Create(newAreaCodePhoneNumber));
            contact.Update(newContactName, newContactEmail, newContactPhone);

            // Assert
            contact.ContactName.Should().Be(newContactName);
            contact.ContactEmail.Should().Be(newContactEmail);
            contact.ContactPhone.Should().Be(newContactPhone);
        }
    }
}