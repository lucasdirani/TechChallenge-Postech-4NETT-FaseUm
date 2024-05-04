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
    }
}