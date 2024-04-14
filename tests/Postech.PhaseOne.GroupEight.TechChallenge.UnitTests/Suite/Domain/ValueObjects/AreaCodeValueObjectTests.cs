using FluentAssertions;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;
using System.ComponentModel;

namespace Postech.PhaseOne.GroupEight.TechChallenge.UnitTests.Suite.Domain.ValueObjects
{
    public class AreaCodeValueObjectTests
    {
        [Theory(DisplayName = "Constructing a valid AreaCode object")]
        [Category("AreaCode")]
        [InlineData("62", RegionValueObject.Goias)]
        [InlineData("82", RegionValueObject.Alagoas)]
        [InlineData("68", RegionValueObject.Acre)]
        [InlineData("11", RegionValueObject.SaoPaulo)]
        [InlineData("41", RegionValueObject.Parana)]
        public void AreaCode_ValidAreaCodeData_ShouldConstructAreaCodeObject(string areaCodeValue, RegionValueObject areaCodeRegion)
        {
            AreaCodeValueObject areaCode = new(areaCodeValue, areaCodeRegion);
            areaCode.Value.Should().Be(areaCodeValue);
            areaCode.Region.Should().Be(areaCodeRegion);
        }

        [Theory(DisplayName = "Constructing an AreaCode object with an invalid value")]
        [Category("AreaCode")]
        [InlineData("", RegionValueObject.Goias)]
        [InlineData(" ", RegionValueObject.Alagoas)]
        [InlineData(null, RegionValueObject.Acre)]
        [InlineData("100", RegionValueObject.SaoPaulo)]
        [InlineData("4", RegionValueObject.Parana)]
        [InlineData("A9", RegionValueObject.Sergipe)]
        public void AreaCode_InvalidAreaCodeData_ShouldThrowAreaCodeValueException(string areaCodeValue, RegionValueObject areaCodeRegion)
        {
            AreaCodeValueException exception = Assert.Throws<AreaCodeValueException>(() => new AreaCodeValueObject(areaCodeValue, areaCodeRegion));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.Value.Should().Be(areaCodeValue);
        }
    }
}