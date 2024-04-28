using FluentAssertions;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.UnitTests.Suite.Domain.ValueObjects
{
    public class ContactEmailValueObjectTests
    {
        [Theory(DisplayName = "Constructing a valid object of type ContactEmailValueObject")]
        [InlineData("lucas.dirani@gmail.com")]
        [InlineData("rfulgencio3@gmail.com")]
        [InlineData("breno.jgomes2@gmail.com")]
        [InlineData("lucaspinho101@gmail.com")]
        [InlineData("tatidornel@gmail.com")]
        [Trait("Action", "ContactEmailValueObject")]
        public void ContactEmailValueObject_ValidData_ShouldConstructContactEmailValueObject(string value)
        {
            ContactEmailValueObject contactEmail = new(value);
            contactEmail.Should().NotBeNull();
            contactEmail.Value.Should().Be(value);
        }

        [Theory(DisplayName = "Constructing an object of type ContactEmailValueObject with the email address in an invalid format")]
        [InlineData("lucas dirani@gmail.com")]
        [InlineData("rfulgencio3@")]
        [InlineData("breno.jgomes2@gmail")]
        [InlineData("lucaspinho101gmail.com")]
        [InlineData("@gmail.com")]
        [InlineData("")]
        [InlineData(" ")]
        [Trait("Action", "ContactEmailValueObject")]
        public void ContactEmailValueObject_InvalidEmailAddress_ShouldThrowContactEmailAddressException(string value)
        {
            ContactEmailAddressException exception = Assert.Throws<ContactEmailAddressException>(() => new ContactEmailValueObject(value));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.EmailAddressValue.Should().Be(value);
        }
    }
}