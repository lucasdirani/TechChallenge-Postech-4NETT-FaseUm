using Moq;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ViewModels;
using Postech.PhaseOne.GroupEight.TechChallenge.FunctionalTests.Fakers.Domain.Entities;

namespace Postech.PhaseOne.GroupEight.TechChallenge.FunctionalTests.StepDefinitions
{
    [Binding]
    public class FindContactByAreaCodeStepDefinitions
    {
        private readonly Mock<IContactRepository> _repository = new();
        private string _areaCodeChosenForTheSearch;
        private IList<ContactEntity> _registeredContacts;
        private FindContactByAreaCodeHandler _handler;
        private DefaultOutput<IEnumerable<FindContactByAreaCodeViewModel>> _findContactOutput;

        [Given(@"that the contact administrator wants to consult all contacts registered with area code ""([^""]*)""")]
        public void GivenThatTheContactAdministratorWantsToConsultAllContactsRegisteredWithAreaCode(string areaCode)
        {
            _areaCodeChosenForTheSearch = areaCode;
        }

        [Given(@"the contact administrator has (.*) active contacts registered with area code ""([^""]*)""")]
        public void GivenTheContactAdministratorHasActiveContactsRegisteredWithAreaCode(int registeredContactsCount, string areaCode)
        {
            _registeredContacts = new ContactEntityFaker(areaCode).Generate(registeredContactsCount);
            _repository.Setup(r => r.GetContactsByAreaCodeValueAsync(areaCode)).ReturnsAsync(_registeredContacts);
        }

        [When(@"the contact administrator searches contacts by area code")]
        public async Task WhenTheContactAdministratorSearchesContactsByAreaCode()
        {
            FindContactInput input = new() { AreaCodeValue = _areaCodeChosenForTheSearch };
            _handler = new(_repository.Object);
            _findContactOutput = await _handler.Handle(input, CancellationToken.None);
        }

        [Then(@"the contact administrator must view the data of the contacts registered with the selected area code")]
        public void ThenTheContactAdministratorMustViewTheDataOfTheContactsRegisteredWithTheSelectedAreaCode()
        {
            _findContactOutput.Success.Should().BeTrue();
            _findContactOutput.Message.Should().NotBeNullOrEmpty();
            _findContactOutput.Data.Should().HaveSameCount(_registeredContacts);
        }
    }
}