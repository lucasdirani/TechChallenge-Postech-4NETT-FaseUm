using Moq;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.FunctionalTests.StepDefinitions
{
    [Binding]
    public class DeleteContactStepDefinitions
    {
        private readonly Mock<IContactRepository> _contactRepository = new();
        private ContactNameValueObject _contactName;
        private ContactEmailValueObject _contactEmail;
        private ContactPhoneValueObject _contactPhone;
        private ContactEntity _contact;
        private DeleteContactHandler _handler;
        private DefaultOutput _deleteContactOutput;

        [Given(@"that the name of the registered contact is ""([^""]*)"" ""([^""]*)""")]
        public void GivenThatTheNameOfTheRegisteredContactIs(string contactFirstName, string contactLastName)
        {
            _contactName = new(contactFirstName, contactLastName);
        }

        [Given(@"the email address of the registered contact is ""([^""]*)""")]
        public void GivenTheEmailAddressOfTheRegisteredContactIs(string contactEmailAddress)
        {
            _contactEmail = new(contactEmailAddress);
        }

        [Given(@"the phone number of the registered contact is ""([^""]*)""")]
        public void GivenThePhoneNumberOfTheRegisteredContactIs(string contactPhoneNumber)
        {
            _contactPhone = ContactPhoneValueObject.Create(contactPhoneNumber);
        }

        [Given(@"the registered contact is active in the contact list")]
        public void GivenTheRegisteredContactIsActiveInTheContactList()
        {
            _contact = new(_contactName, _contactEmail, _contactPhone);
            _contactRepository.Setup(c => c.GetByIdAsync(_contact.Id)).ReturnsAsync(_contact);
        }

        [When(@"the contact administrator chooses to delete this registered contact")]
        public async Task WhenTheContactAdministratorChoosesToDeleteThisRegisteredContact()
        {
            DeleteContactInput input = new() { ContactId = _contact.Id };
            _handler = new(_contactRepository.Object);
            _deleteContactOutput = await _handler.Handle(input, CancellationToken.None);
        }

        [Then(@"the registered contact must be deleted")]
        public void ThenTheRegisteredContactMustBeDeleted()
        {
            _deleteContactOutput.Success.Should().BeTrue();
            _contact.IsActive().Should().BeFalse();
            _contact.ModifiedAt.Should().BeOnOrBefore(DateTime.UtcNow);
            _contactRepository.Verify(c => c.SaveChangesAsync(), Times.Once());
        }
    }
}