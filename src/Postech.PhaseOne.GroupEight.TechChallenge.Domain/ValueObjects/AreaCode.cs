using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects
{
    public record AreaCode
    {
        public const int AreaCodeValueLength = 2;

        public AreaCode(string areaCodeValue, AreaCodeRegion areaCodeRegion)
        {
            if (string.IsNullOrEmpty(areaCodeValue) 
                || areaCodeValue.Length != AreaCodeValueLength
                || areaCodeValue.Any(digit => !char.IsDigit(digit)))
            {
                throw new AreaCodeValueException("The area code value must be a two-digit number", areaCodeValue);
            }
            Value = areaCodeValue;
            Region = areaCodeRegion;
        }

        public string Value { get; init; }
        public AreaCodeRegion Region { get; init; }
    }
}