using FluentAssertions;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.UnitTests.Suite.Domain.ValueObjects
{
    public class ContactNameValueObjectTests
    {
        [Theory(DisplayName = "Constructing a valid object of type ContactNameValueObject")]
        [InlineData("Lucas", "Dirani")]
        [InlineData("Ricardo", "Fulgencio")]
        [InlineData("Breno", "Jhefferson")]
        [InlineData("Lucas", "Montarroyos")]
        [InlineData("Tatiana", "Lima")]
        [InlineData("Antônio", "Pereira")]
        [InlineData("Leila", "Alves Gomes")]
        [Trait("Action", "ContactNameValueObject")]
        public void ContactNameValueObject_ValidData_ShouldConstructContactNameValueObject(string firstName, string lastName)
        {
            ContactNameValueObject contactName = new(firstName, lastName);
            contactName.Should().NotBeNull();
            contactName.FirstName.Should().Be(firstName);
            contactName.LastName.Should().Be(lastName);
        }

        [Theory(DisplayName = "Constructing an object of type ContactNameValueObject with the first name in an invalid format")]
        [InlineData("", "Dirani")]
        [InlineData(" ", "Fulgencio")]
        [InlineData("Br4no", "Jhefferson")]
        [InlineData("Luc?s", "Montarroyos")]
        [InlineData("Ta[tiana", "Lima")]
        [InlineData("Juan Mendes", "Silva")]
        [Trait("Action", "ContactNameValueObject")]
        public void ContactNameValueObject_InvalidFirstName_ShouldThrowContactFirstNameException(string firstName, string lastName)
        {
            ContactFirstNameException exception = Assert.Throws<ContactFirstNameException>(() => new ContactNameValueObject(firstName, lastName));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.FirstNameValue.Should().Be(firstName);
        }

        [Theory(DisplayName = "Constructing an object of type ContactNameValueObject with the last name in an invalid format")]
        [InlineData("Lucas", "")]
        [InlineData("Ricardo", " ")]
        [InlineData("Breno", "Jh!fferson")]
        [InlineData("Lucas", "[ontarroyos")]
        [InlineData("Tatiana", "Lim@")]
        [InlineData("Juan", "Silva ")]
        [InlineData("Leila", "Alves Gom^s")]
        [Trait("Action", "ContactNameValueObject")]
        public void ContactNameValueObject_InvalidLastName_ShouldThrowContactLastNameException(string firstName, string lastName)
        {
            ContactLastNameException exception = Assert.Throws<ContactLastNameException>(() => new ContactNameValueObject(firstName, lastName));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.LastNameValue.Should().Be(lastName);
        }
    }
}