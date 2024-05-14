using Bogus;
using FluentAssertions;
using Moq;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ViewModels;
using System.Linq.Expressions;

namespace Postech.PhaseOne.GroupEight.TechChallenge.UnitTests.Suite.Domain.Handlers.Contacts
{
    public class FindContactHandlerTests
    {
        private readonly Faker _faker = new("pt_BR");

        [Theory(DisplayName = "Fetching list of contacts by area code.")]
        [InlineData("11")]
        [InlineData("31")]
        [InlineData("81")]
        [Trait("Action", "Handle")]
        public async Task Handle_FetchingExistentContactsByAreaCode_ShouldReturnContactsList(string InputAreaCodeValue)
        {
            /*
            var contactNames = new AutoFaker<ContactNameValueObject>()
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .Generate(10);
            */


            ContactNameValueObject contactNameToBeFound1 = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject contactEmailToBeFound1 = new(_faker.Internet.Email());
            ContactPhoneValueObject contactPhoneToBeFound1 = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create(InputAreaCodeValue));
            ContactEntity contactToBeFound1 = new(contactNameToBeFound1, contactEmailToBeFound1, contactPhoneToBeFound1);

            ContactNameValueObject contactNameToBeFound2 = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject contactEmailToBeFound2 = new(_faker.Internet.Email());
            ContactPhoneValueObject contactPhoneToBeFound2 = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create(InputAreaCodeValue));
            ContactEntity contactToBeFound2 = new(contactNameToBeFound2, contactEmailToBeFound2, contactPhoneToBeFound2);

            var contactList = new List<ContactEntity>
            {
                 contactToBeFound1,
                 contactToBeFound2

            };

            Expression<Func<ContactEntity, bool>> expression = contact =>
                   contact.ContactPhone.AreaCode.Value == InputAreaCodeValue
                   && contact.Active;

            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.FindAsync(expression))
                                  .ReturnsAsync(contactList);

            FindContactByAreaCodeHandler handler = new(contactRepository.Object);
            FindContactInput input = new() { AreaCodeValue = InputAreaCodeValue };

            DefaultOutput output = await handler.Handle(input, CancellationToken.None);

            output.Success.Should().Be(true);
            output.Data.Should().NotBeNull();
            output.Data.As<IEnumerable<FindContactByAreaCodeViewModel>>().Should().HaveCount(2);
        }

        [Theory(DisplayName = "Fetching list of contacts by non-existent area code.")]
        [InlineData("11")]
        [InlineData("31")]
        [InlineData("81")]
        [Trait("Action", "Handle")]
        public async Task Handle_FetchingNonExistentContactsByAreaCode_ShouldReturnEmptyList(string InputAreaCodeValue)
        {
            ContactNameValueObject contactNameToNotBeFound = new(_faker.Name.FirstName(), _faker.Name.LastName());
            ContactEmailValueObject contactEmailToNotBeFound = new(_faker.Internet.Email());
            ContactPhoneValueObject contactPhoneToNotBeFound = new(_faker.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create("00"));
            ContactEntity contactToNotBeFound = new(contactNameToNotBeFound, contactEmailToNotBeFound, contactPhoneToNotBeFound);

            var contactList = new List<ContactEntity>
            {
                 contactToNotBeFound
            };

            Expression<Func<ContactEntity, bool>> expression = contact =>
                   contact.ContactPhone.AreaCode.Value == InputAreaCodeValue
                   && contact.Active;

            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.FindAsync(expression))
                                  .ReturnsAsync(new List<ContactEntity>());

            FindContactByAreaCodeHandler handler = new(contactRepository.Object);
            FindContactInput input = new() { AreaCodeValue = InputAreaCodeValue };

            DefaultOutput output = await handler.Handle(input, CancellationToken.None);

            output.Success.Should().Be(true);
            output.Data.Should().NotBeNull();
            output.Data.As<IEnumerable<FindContactByAreaCodeViewModel>>().Should().BeEmpty();
        }
    }
}