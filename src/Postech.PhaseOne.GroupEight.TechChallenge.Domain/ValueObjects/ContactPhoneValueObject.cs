using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects;
using System.Text.RegularExpressions;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects
{
    public partial record ContactPhoneValueObject
    {
        public ContactPhoneValueObject(string phoneNumber, AreaCodeValueObject? areaCode)
            : this(phoneNumber)
        {
            NotFoundException.ThrowWhenNullEntity(areaCode, "The area code is required");
            AreaCode = areaCode;
        }

        private ContactPhoneValueObject(string number)
        {
            ContactPhoneNumberException.ThrowIfFormatIsInvalid(number, PhoneNumberRegex());
            Number = number;
        }

        public string Number { get; init; }
        public AreaCodeValueObject AreaCode { get; init; }

        [GeneratedRegex("^[2-9][0-9]{3,4}[0-9]{4}$", RegexOptions.Compiled)]
        private static partial Regex PhoneNumberRegex();
    }
}