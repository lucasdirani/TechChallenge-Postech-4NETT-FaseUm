using FluentAssertions;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.UnitTests.Suite.Domain.ValueObjects
{
    public class AreaCodeValueObjectTests
    {
        [Theory(DisplayName = "Constructing a valid object of type AreaCodeValueObject")]
        [MemberData(nameof(GetAreaCodeValues))]
        [Trait("Action", "Create")]
        public void Create_ValidData_ShouldCreateAreaCodeValueObject(string areaCodeValue)
        {
            AreaCodeValueObject areaCodeValueObject = AreaCodeValueObject.Create(areaCodeValue);
            areaCodeValueObject.Should().NotBeNull();
            areaCodeValueObject.Value.Should().Be(areaCodeValue);
            areaCodeValueObject.Region.Should().NotBeNull();
        }

        [Theory(DisplayName = "Construct an object of type AreaCodeValueObject with an unsupported area code value")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("01")]
        [InlineData("90")]
        [InlineData("100")]
        [InlineData("1000")]
        [InlineData("D1")]
        [Trait("Action", "Create")]
        public void Create_NotSupportedAreaCodeValue_ShouldThrowAreaCodeValueNotSupportedException(string areaCodeValue)
        {
            AreaCodeValueNotSupportedException exception = Assert.Throws<AreaCodeValueNotSupportedException>(() => AreaCodeValueObject.Create(areaCodeValue));
            exception.Message.Should().NotBeNullOrEmpty();
            exception.AreaCodeValue.Should().Be(areaCodeValue);
        }

        public static TheoryData<string> GetAreaCodeValues()
        {
            return new TheoryData<string>
            {
                "11", "12", "13", "14", "15", "16", "17", "18", "19",
                "21", "22", "24", "27", "28", "31", "32", "33", "34",
                "35", "37", "38", "41", "42", "43", "44", "45", "46",
                "47", "48", "49", "51", "53", "54", "55", "61", "62",
                "63", "64", "65", "66", "67", "68", "69", "71", "73",
                "74", "75", "77", "79", "81", "82", "83", "84", "85",
                "86", "87", "88", "89", "91", "92", "93", "94", "95",
                "96", "97", "98", "99"
            };
        }
    }
}