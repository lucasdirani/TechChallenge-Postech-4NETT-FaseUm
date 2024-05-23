using Bogus;
using FluentAssertions;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.UnitTests.Suite.Domain.ValueObjects
{
    public class ContactNameValueObjectTests
    {
        private readonly Faker _faker = new("pt_BR");

        [Theory(DisplayName = "Constructing a valid object of type ContactNameValueObject")]
        [InlineData("Lucas", "Dirani")]
        [InlineData("Ricardo", "Fulgencio")]
        [InlineData("Breno", "Jhefferson")]
        [InlineData("Lucas", "Montarroyos")]
        [InlineData("Tatiana", "Lima")]
        [InlineData("Antônio", "Pereira")]
        [InlineData("Júlio Cesar", "Antunes")]
        [InlineData("Leila", "Alves Gomes")]
        [InlineData("José Paulo", "Bezerra Maciel Júnior")]
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
        [InlineData("Maria Aparecida Benedita Francisca da Conceição", "Lima")]
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
        [InlineData("Cassio", "Antônio Carlos Eduardo Francisco Gabriel da Silva Oliveira Pereira")]
        [Trait("Action", "ContactNameValueObject")]
        public void ContactNameValueObject_InvalidLastName_ShouldThrowContactLastNameException(string firstName, string lastName)
        {
            ContactLastNameException exception = Assert.Throws<ContactLastNameException>(() => new ContactNameValueObject(firstName, lastName));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.LastNameValue.Should().Be(lastName);
        }

        [Theory(DisplayName = "Contact's first name is being changed.")]
        [InlineData("Lucas", "Lucca")]
        [InlineData("Tatiana", "Tathiana")]
        [Trait("Action", "HasBeenChanged")]
        public void HasBeenChanged_ContactFirstNameHasBeenChanged_ShouldReturnTrue(string currentFirstName, string otherFirstName)
        {
            ContactNameValueObject contactName = new(currentFirstName, _faker.Name.LastName());
            contactName.HasBeenChanged(otherFirstName, contactName.LastName).Should().BeTrue();
        }

        [Theory(DisplayName = "Contact's last name is being changed.")]
        [InlineData("Ruiz", "Dirani")]
        [InlineData("Silva", "Santos")]
        [Trait("Action", "HasBeenChanged")]
        public void HasBeenChanged_ContactLastNameHasBeenChanged_ShouldReturnTrue(string currentLastName, string otherLastName)
        {
            ContactNameValueObject contactName = new(_faker.Name.FirstName(), currentLastName);
            contactName.HasBeenChanged(contactName.FirstName, otherLastName).Should().BeTrue();
        }

        [Theory(DisplayName = "Contact's first name and last name are being changed.")]
        [InlineData("Lucas", "Dirani", "Lucca", "Ruiz")]
        [InlineData("Cristiane", "Jesus", "Cristina", "Andrade")]
        [InlineData("Rafael", "Silva", "Raphael", "Souza")]
        [Trait("Action", "HasBeenChanged")]
        public void HasBeenChanged_ContactFirstNameAndLastNameHaveBeenChanged_ShouldReturnTrue(
            string currentFirstName, 
            string currentLastName,
            string otherFirstName,
            string otherLastName)
        {
            ContactNameValueObject contactName = new(currentFirstName, currentLastName);
            contactName.HasBeenChanged(otherFirstName, otherLastName).Should().BeTrue();
        }

        [Theory(DisplayName = "Contact's first name and last name are not being changed.")]
        [InlineData("Lucas", "Dirani", "Lucas", "Dirani")]
        [InlineData("Cristiane", "Jesus", "Cristiane", "Jesus")]
        [InlineData("Rafael", "Silva", "Rafael", "Silva")]
        [Trait("Action", "HasBeenChanged")]
        public void HasBeenChanged_ContactFirstNameAndLastNameHaveNotBeenChanged_ShouldReturnFalse(
            string currentFirstName,
            string currentLastName,
            string otherFirstName,
            string otherLastName)
        {
            ContactNameValueObject contactName = new(currentFirstName, currentLastName);
            contactName.HasBeenChanged(otherFirstName, otherLastName).Should().BeFalse();
        }
    }
}