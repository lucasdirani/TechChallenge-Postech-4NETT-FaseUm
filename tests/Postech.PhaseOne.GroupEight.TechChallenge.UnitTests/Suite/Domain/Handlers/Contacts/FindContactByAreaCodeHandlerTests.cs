using FluentAssertions;
using Moq;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ViewModels;
using Postech.PhaseOne.GroupEight.TechChallenge.UnitTests.Fakers.Domain.Entities;

namespace Postech.PhaseOne.GroupEight.TechChallenge.UnitTests.Suite.Domain.Handlers.Contacts
{
    public class FindContactByAreaCodeHandlerTests
    {
        [Theory(DisplayName = "Fetching list of contacts by area code.")]
        [InlineData("11")]
        [InlineData("31")]
        [InlineData("81")]
        [Trait("Action", "Handle")]
        public async Task Handle_FetchingExistentContactsByAreaCode_ShouldReturnContactsByAreaCode(string areaCode)
        {
            // Arrange
            List<ContactEntity> contacts = new ContactEntityFaker(areaCode).Generate(10);
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.GetContactsByAreaCodeValueAsync(areaCode)).ReturnsAsync(contacts);
            FindContactByAreaCodeHandler handler = new(contactRepository.Object);
            FindContactInput input = new() { AreaCodeValue = areaCode };

            // Act
            DefaultOutput<IEnumerable<FindContactByAreaCodeViewModel>> output = await handler.Handle(input, CancellationToken.None);

            // Assert
            output.Success.Should().Be(true);
            output.Data.Should().NotBeNull();
            output.Data.As<IEnumerable<FindContactByAreaCodeViewModel>>().Should().HaveSameCount(contacts);
        }

        [Theory(DisplayName = "Fetching list of contacts by non-existent area code.")]
        [InlineData("11")]
        [InlineData("31")]
        [InlineData("81")]
        [Trait("Action", "Handle")]
        public async Task Handle_NoContactsRegisteredForTheRequestedAreaCode_ShouldThrowNotFoundException(string areaCode)
        {
            // Arrange
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.GetContactsByAreaCodeValueAsync(areaCode)).ReturnsAsync([]);
            FindContactByAreaCodeHandler handler = new(contactRepository.Object);
            FindContactInput input = new() { AreaCodeValue = areaCode };

            // Assert
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(input, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
        }
    }
}