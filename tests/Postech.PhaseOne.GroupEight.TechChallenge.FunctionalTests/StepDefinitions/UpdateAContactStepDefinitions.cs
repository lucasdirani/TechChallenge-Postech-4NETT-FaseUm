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
    public class UpdateAContactStepDefinitions
    {
        private ContactNameValueObject _registeredContactName;
        private ContactEmailValueObject _registeredContactEmail;
        private ContactPhoneValueObject _registeredContactPhone;
        private ContactEntity _registeredContact;
        private string _contactNewFirstName;
        private string _contactNewLastName;
        private string _contactNewEmailAddress;
        private string _contactNewPhoneNumber;
        private string _contactNewAreaCodePhoneNumber;
        private readonly Mock<IContactRepository> _repository = new();
        private readonly Mock<IRegisteredContactChecker> _checker = new();
        private readonly Mock<IContactPhoneValueObjectFactory> _contactPhoneFactory = new();
        private UpdateContactHandler _handler;
        private DefaultOutput<UpdateContactViewModel> _updateContactOutput;

        [Given(@"that the provided name of the registered contact is ""([^""]*)"" ""([^""]*)""")]
        public void GivenThatTheProvidedNameOfTheRegisteredContactIs(string contactFirstName, string contactLastName)
        {
            _registeredContactName = new(contactFirstName, contactLastName);
        }

        [Given(@"the provided email address of the registered contact is ""([^""]*)""")]
        public void GivenTheProvidedEmailAddressOfTheRegisteredContactIs(string contactEmailAddress)
        {
            _registeredContactEmail = new(contactEmailAddress);
        }

        [Given(@"the provided phone number of the registered contact is ""([^""]*)""")]
        public void GivenTheProvidedPhoneNumberOfTheRegisteredContactIs(string contactPhoneNumber)
        {
            _registeredContactPhone = ContactPhoneValueObject.Create(contactPhoneNumber);
        }

        [Given(@"the registered contact is current active")]
        public void GivenTheRegisteredContactIsCurrentActive()
        {
            _registeredContact = new(_registeredContactName, _registeredContactEmail, _registeredContactPhone);
            _repository.Setup(r => r.GetByIdAsync(_registeredContact.Id)).ReturnsAsync(_registeredContact);
        }

        [Given(@"the contact administrator changes the first name of the registered contact to ""([^""]*)""")]
        public void GivenTheContactAdministratorChangesTheFirstNameOfTheRegisteredContactTo(string contactFirstName)
        {
            _contactNewFirstName = contactFirstName;
        }

        [Given(@"the contact administrator changes the last name of the registered contact to ""([^""]*)""")]
        public void GivenTheContactAdministratorChangesTheLastNameOfTheRegisteredContactTo(string contactLastName)
        {
            _contactNewLastName = contactLastName;
        }

        [Given(@"the contact administrator changes the email address of the registered contact to ""([^""]*)""")]
        public void GivenTheContactAdministratorChangesTheEmailAddressOfTheRegisteredContactTo(string contactEmailAddress)
        {
            _contactNewEmailAddress = contactEmailAddress;
        }

        [Given(@"the contact administrator changes the phone number of the registered contact to ""([^""]*)""")]
        public void GivenTheContactAdministratorChangesThePhoneNumberOfTheRegisteredContactTo(string contactPhoneNumber)
        {
            ContactPhoneValueObject contactNewPhone = ContactPhoneValueObject.Create(contactPhoneNumber);
            _contactNewPhoneNumber = contactNewPhone.Number;
            _contactNewAreaCodePhoneNumber = contactNewPhone.AreaCode.Value;
            _contactPhoneFactory.Setup(f => f.CreateAsync(_contactNewPhoneNumber, _contactNewAreaCodePhoneNumber)).ReturnsAsync(contactNewPhone);
        }

        [Given(@"the contact with the updated data has not yet been registered by the contact administrator")]
        public void GivenTheContactWithTheUpdatedDataHasNotYetBeenRegisteredByTheContactAdministrator()
        {
            _checker.Setup(c => c.CheckRegisteredContactAsync(It.Is<ContactEntity>(contact => contact.Id.Equals(_registeredContact.Id)))).ReturnsAsync(false);
            _repository.Setup(r => r.Update(It.Is<ContactEntity>(contact => contact.Id.Equals(_registeredContact.Id))));
            _repository.Setup(r => r.SaveChangesAsync());
        }

        [When(@"the contact administrator confirms the change in registered contact data")]
        public async Task WhenTheContactAdministratorConfirmsTheChangeInRegisteredContactData()
        {
            UpdateContactInput input = new()
            {
                ContactEmail = _contactNewEmailAddress,
                ContactFirstName = _contactNewFirstName,
                ContactId = _registeredContact.Id,
                ContactLastName = _contactNewLastName,
                ContactPhoneNumber = _contactNewPhoneNumber,
                ContactPhoneNumberAreaCode = _contactNewAreaCodePhoneNumber,
            };
            _handler = new(_repository.Object, _contactPhoneFactory.Object, _checker.Object);
            _updateContactOutput = await _handler.Handle(input, CancellationToken.None);
        }

        [Then(@"the registered contact data must be updated")]
        public void ThenTheRegisteredContactDataMustBeUpdated()
        {
            _updateContactOutput.Success.Should().BeTrue();
            _updateContactOutput.Message.Should().NotBeNullOrEmpty();
            _updateContactOutput.Data.ContactPhoneNumber.Should().Be(_contactNewPhoneNumber);
            _updateContactOutput.Data.ContactId.Should().Be(_registeredContact.Id);
            _updateContactOutput.Data.ContactLastName.Should().Be(_contactNewLastName);
            _updateContactOutput.Data.ContactFirstName.Should().Be(_contactNewFirstName);
            _updateContactOutput.Data.ContactEmail.Should().Be(_contactNewEmailAddress);
            _updateContactOutput.Data.ContactPhoneAreaCode.Should().Be(_contactNewAreaCodePhoneNumber);
        }
    }
}