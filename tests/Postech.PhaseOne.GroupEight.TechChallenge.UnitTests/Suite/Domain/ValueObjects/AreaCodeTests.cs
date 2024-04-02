using FluentAssertions;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;
using System.ComponentModel;

namespace Postech.PhaseOne.GroupEight.TechChallenge.UnitTests.Suite.Domain.ValueObjects
{
    public class AreaCodeTests
    {
        [Theory(DisplayName = "Constructing a valid AreaCode object")]
        [Category("AreaCode")]
        [InlineData("62", AreaCodeRegion.Goias)]
        [InlineData("82", AreaCodeRegion.Alagoas)]
        [InlineData("68", AreaCodeRegion.Acre)]
        [InlineData("11", AreaCodeRegion.SaoPaulo)]
        [InlineData("41", AreaCodeRegion.Parana)]
        public void AreaCode_ValidAreaCodeData_ShouldConstructAreaCodeObject(string areaCodeValue, AreaCodeRegion areaCodeRegion)
        {
            AreaCode areaCode = new(areaCodeValue, areaCodeRegion);
            areaCode.Value.Should().Be(areaCodeValue);
            areaCode.Region.Should().Be(areaCodeRegion);
        }

        [Theory(DisplayName = "Constructing an AreaCode object with an invalid value")]
        [Category("AreaCode")]
        [InlineData("", AreaCodeRegion.Goias)]
        [InlineData(" ", AreaCodeRegion.Alagoas)]
        [InlineData(null, AreaCodeRegion.Acre)]
        [InlineData("100", AreaCodeRegion.SaoPaulo)]
        [InlineData("4", AreaCodeRegion.Parana)]
        [InlineData("A9", AreaCodeRegion.Sergipe)]
        public void AreaCode_InvalidAreaCodeData_ShouldThrowAreaCodeValueException(string areaCodeValue, AreaCodeRegion areaCodeRegion)
        {
            AreaCodeValueException exception = Assert.Throws<AreaCodeValueException>(() => new AreaCode(areaCodeValue, areaCodeRegion));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.Value.Should().Be(areaCodeValue);
        }
    }
}