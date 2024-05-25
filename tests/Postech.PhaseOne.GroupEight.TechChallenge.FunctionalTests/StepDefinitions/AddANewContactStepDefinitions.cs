using Moq;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Checkers.Interfaces;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Factories.Interfaces;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ViewModels;

namespace Postech.PhaseOne.GroupEight.TechChallenge.FunctionalTests.StepDefinitions
{
    [Binding]
    public class AddANewContactStepDefinitions
    {
        private readonly Mock<IContactRepository> _repository = new();
        private readonly Mock<IRegisteredContactChecker> _checker = new();
        private readonly Mock<IContactPhoneValueObjectFactory> _contactPhoneFactory = new();
        private ContactNameValueObject _contactName;
        private ContactEmailValueObject _contactEmail;
        private ContactPhoneValueObject _contactPhone;
        private ContactEntity _contact;
        private AddNewContactHandler _handler;
        private DefaultOutput<AddNewContactViewModel> _addContactOutput;

        [Given(@"that the name of the new contact is ""([^""]*)"" ""([^""]*)""")]
        public void GivenThatTheNameOfTheNewContactIs(string contactFirstName, string contactLastName)
        {
            _contactName = new ContactNameValueObject(contactFirstName, contactLastName);
        }

        [Given(@"the email address of the new contact is ""([^""]*)""")]
        public void GivenTheEmailAddressOfTheNewContactIs(string contactEmailAddress)
        {
            _contactEmail = new ContactEmailValueObject(contactEmailAddress);
        }

        [Given(@"the phone number of the new contact is ""([^""]*)""")]
        public void GivenThePhoneNumberOfTheNewContactIs(string contactPhoneNumber)
        {
            _contactPhone = ContactPhoneValueObject.Create(contactPhoneNumber);
            _contactPhoneFactory.Setup(f => f.CreateAsync(_contactPhone.Number, _contactPhone.AreaCode.Value)).ReturnsAsync(_contactPhone);
        }

        [Given(@"the new contact has not yet been registered by the contact administrator")]
        public void GivenTheNewContactHasNotYetBeenRegisteredByTheContactAdministrator()
        {
            _contact = new(_contactName, _contactEmail, _contactPhone);
            _checker.Setup(c => c.CheckRegisteredContactAsync(_contact)).ReturnsAsync(false);
            _repository.Setup(r => r.InsertAsync(_contact));
            _repository.Setup(r => r.SaveChangesAsync());
        }

        [When(@"the contact administrator registers the new contact")]
        public async Task WhenTheContactAdministratorRegistersTheNewContact()
        {
            AddContactInput input = new()
            {
                ContactEmail = _contactEmail.Value,
                ContactFirstName = _contactName.FirstName,
                ContactLastName = _contactName.LastName,
                ContactPhoneNumber = _contactPhone.Number,
                ContactPhoneNumberAreaCode = _contactPhone.AreaCode.Value
            };
            _handler = new(_repository.Object, _contactPhoneFactory.Object, _checker.Object);
            _addContactOutput = await _handler.Handle(input, CancellationToken.None);
        }

        [Then(@"the new contact must be registered successfully")]
        public void ThenTheNewContactMustBeRegisteredSuccessfully()
        {
            _addContactOutput.Success.Should().BeTrue();
            _addContactOutput.Message.Should().NotBeNullOrEmpty();
            _addContactOutput.Data.ContactId.Should().NotBeEmpty();
            _addContactOutput.Data.ContactFirstName.Should().Be(_contactName.FirstName);
            _addContactOutput.Data.ContactLastName.Should().Be(_contactName.LastName);
            _addContactOutput.Data.ContactEmail.Should().Be(_contactEmail.Value);
            _addContactOutput.Data.ContactPhoneNumber.Should().Be(_contactPhone.Number);
            _addContactOutput.Data.ContactPhoneAreaCode.Should().Be(_contactPhone.AreaCode.Value);
        }
    }
}