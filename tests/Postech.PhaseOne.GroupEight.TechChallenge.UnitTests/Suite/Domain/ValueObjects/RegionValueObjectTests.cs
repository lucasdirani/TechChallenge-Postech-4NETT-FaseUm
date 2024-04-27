using FluentAssertions;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Enumerators;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.UnitTests.Suite.Domain.ValueObjects
{
    public class RegionValueObjectTests
    {
        [Theory(DisplayName = "Constructing a valid object of type RegionValueObject")]
        [InlineData(RegionNameEnumerator.Midwest, RegionStateNameEnumerator.DistritoFederal)]
        [InlineData(RegionNameEnumerator.Midwest, RegionStateNameEnumerator.Goias)]
        [InlineData(RegionNameEnumerator.Midwest, RegionStateNameEnumerator.MatoGrosso)]
        [InlineData(RegionNameEnumerator.Midwest, RegionStateNameEnumerator.MatoGrossoDoSul)]
        [InlineData(RegionNameEnumerator.Northeast, RegionStateNameEnumerator.Alagoas)]
        [InlineData(RegionNameEnumerator.Northeast, RegionStateNameEnumerator.Bahia)]
        [InlineData(RegionNameEnumerator.Northeast, RegionStateNameEnumerator.Ceara)]
        [InlineData(RegionNameEnumerator.Northeast, RegionStateNameEnumerator.Maranhao)]
        [InlineData(RegionNameEnumerator.Northeast, RegionStateNameEnumerator.Paraiba)]
        [InlineData(RegionNameEnumerator.Northeast, RegionStateNameEnumerator.Pernambuco)]
        [InlineData(RegionNameEnumerator.Northeast, RegionStateNameEnumerator.Piaui)]
        [InlineData(RegionNameEnumerator.Northeast, RegionStateNameEnumerator.RioGrandeDoNorte)]
        [InlineData(RegionNameEnumerator.Northeast, RegionStateNameEnumerator.Sergipe)]
        [InlineData(RegionNameEnumerator.North, RegionStateNameEnumerator.Acre)]
        [InlineData(RegionNameEnumerator.North, RegionStateNameEnumerator.Amapa)]
        [InlineData(RegionNameEnumerator.North, RegionStateNameEnumerator.Amazonas)]
        [InlineData(RegionNameEnumerator.North, RegionStateNameEnumerator.Para)]
        [InlineData(RegionNameEnumerator.North, RegionStateNameEnumerator.Rondonia)]
        [InlineData(RegionNameEnumerator.North, RegionStateNameEnumerator.Roraima)]
        [InlineData(RegionNameEnumerator.North, RegionStateNameEnumerator.Tocantins)]
        [InlineData(RegionNameEnumerator.Southeast, RegionStateNameEnumerator.EspiritoSanto)]
        [InlineData(RegionNameEnumerator.Southeast, RegionStateNameEnumerator.MinasGerais)]
        [InlineData(RegionNameEnumerator.Southeast, RegionStateNameEnumerator.RioDeJaneiro)]
        [InlineData(RegionNameEnumerator.Southeast, RegionStateNameEnumerator.SaoPaulo)]
        [InlineData(RegionNameEnumerator.South, RegionStateNameEnumerator.Parana)]
        [InlineData(RegionNameEnumerator.South, RegionStateNameEnumerator.RioGrandeDoSul)]
        [InlineData(RegionNameEnumerator.South, RegionStateNameEnumerator.SantaCatarina)]
        [Trait("Action", "RegionValueObject")]
        public void RegionValueObject_ValidData_ShouldConstructRegionValueObject(RegionNameEnumerator regionName, RegionStateNameEnumerator regionStateName)
        {
            RegionValueObject region = new(regionName, regionStateName);
            region.Should().NotBeNull();
            region.RegionName.Should().Be(regionName);
            region.RegionStateName.Should().Be(regionStateName);
        }

        [Theory(DisplayName = "Constructing an object of type RegionValueObject in which the states are not from the Midwest region")]
        [InlineData(RegionNameEnumerator.Midwest, RegionStateNameEnumerator.Alagoas)]
        [InlineData(RegionNameEnumerator.Midwest, RegionStateNameEnumerator.SantaCatarina)]
        [InlineData(RegionNameEnumerator.Midwest, RegionStateNameEnumerator.SaoPaulo)]
        [InlineData(RegionNameEnumerator.Midwest, RegionStateNameEnumerator.Para)]
        [Trait("Action", "RegionValueObject")]
        public void RegionValueObject_StatesThatAreNotFromTheMidwestRegion_ShouldThrowInvalidRegionException(RegionNameEnumerator regionName, RegionStateNameEnumerator regionStateName)
        {
            InvalidRegionException exception = Assert.Throws<InvalidRegionException>(() => new RegionValueObject(regionName, regionStateName));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.RegionName.Should().Be(regionName);
            exception.RegionStateName.Should().Be(regionStateName);
        }

        [Theory(DisplayName = "Constructing an object of type RegionValueObject in which the states are not from the Northeast region")]
        [InlineData(RegionNameEnumerator.Northeast, RegionStateNameEnumerator.DistritoFederal)]
        [InlineData(RegionNameEnumerator.Northeast, RegionStateNameEnumerator.SantaCatarina)]
        [InlineData(RegionNameEnumerator.Northeast, RegionStateNameEnumerator.EspiritoSanto)]
        [InlineData(RegionNameEnumerator.Northeast, RegionStateNameEnumerator.Tocantins)]
        [Trait("Action", "RegionValueObject")]
        public void RegionValueObject_StatesThatAreNotFromTheNortheastRegion_ShouldThrowInvalidRegionException(RegionNameEnumerator regionName, RegionStateNameEnumerator regionStateName)
        {
            InvalidRegionException exception = Assert.Throws<InvalidRegionException>(() => new RegionValueObject(regionName, regionStateName));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.RegionName.Should().Be(regionName);
            exception.RegionStateName.Should().Be(regionStateName);
        }

        [Theory(DisplayName = "Constructing an object of type RegionValueObject in which the states are not from the North region")]
        [InlineData(RegionNameEnumerator.North, RegionStateNameEnumerator.MatoGrosso)]
        [InlineData(RegionNameEnumerator.North, RegionStateNameEnumerator.RioGrandeDoSul)]
        [InlineData(RegionNameEnumerator.North, RegionStateNameEnumerator.RioDeJaneiro)]
        [InlineData(RegionNameEnumerator.North, RegionStateNameEnumerator.Bahia)]
        [Trait("Action", "RegionValueObject")]
        public void RegionValueObject_StatesThatAreNotFromTheNorthRegion_ShouldThrowInvalidRegionException(RegionNameEnumerator regionName, RegionStateNameEnumerator regionStateName)
        {
            InvalidRegionException exception = Assert.Throws<InvalidRegionException>(() => new RegionValueObject(regionName, regionStateName));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.RegionName.Should().Be(regionName);
            exception.RegionStateName.Should().Be(regionStateName);
        }

        [Theory(DisplayName = "Constructing an object of type RegionValueObject in which the states are not from the Southeast region")]
        [InlineData(RegionNameEnumerator.Southeast, RegionStateNameEnumerator.MatoGrossoDoSul)]
        [InlineData(RegionNameEnumerator.Southeast, RegionStateNameEnumerator.Roraima)]
        [InlineData(RegionNameEnumerator.Southeast, RegionStateNameEnumerator.RioGrandeDoSul)]
        [InlineData(RegionNameEnumerator.Southeast, RegionStateNameEnumerator.Pernambuco)]
        [Trait("Action", "RegionValueObject")]
        public void RegionValueObject_StatesThatAreNotFromTheSoutheastRegion_ShouldThrowInvalidRegionException(RegionNameEnumerator regionName, RegionStateNameEnumerator regionStateName)
        {
            InvalidRegionException exception = Assert.Throws<InvalidRegionException>(() => new RegionValueObject(regionName, regionStateName));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.RegionName.Should().Be(regionName);
            exception.RegionStateName.Should().Be(regionStateName);
        }

        [Theory(DisplayName = "Constructing an object of type RegionValueObject in which the states are not from the South region")]
        [InlineData(RegionNameEnumerator.South, RegionStateNameEnumerator.DistritoFederal)]
        [InlineData(RegionNameEnumerator.South, RegionStateNameEnumerator.Rondonia)]
        [InlineData(RegionNameEnumerator.South, RegionStateNameEnumerator.SaoPaulo)]
        [InlineData(RegionNameEnumerator.South, RegionStateNameEnumerator.Piaui)]
        [Trait("Action", "RegionValueObject")]
        public void RegionValueObject_StatesThatAreNotFromTheSouthRegion_ShouldThrowInvalidRegionException(RegionNameEnumerator regionName, RegionStateNameEnumerator regionStateName)
        {
            InvalidRegionException exception = Assert.Throws<InvalidRegionException>(() => new RegionValueObject(regionName, regionStateName));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.RegionName.Should().Be(regionName);
            exception.RegionStateName.Should().Be(regionStateName);
        }
    }
}