using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects;
using System.Text.RegularExpressions;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects
{
    public partial record ContactPhoneValueObject
    {
        public ContactPhoneValueObject(string phoneNumber, AreaCodeValueObject areaCode)
            : this(phoneNumber)
        {
            AreaCode = areaCode;
        }

        private ContactPhoneValueObject(string number)
        {
            if (!PhoneNumberRegex().IsMatch(number))
            {
                throw new ContactPhoneNumberException("The phone number must only have eight or nine digits, and must not contain special characters, such as hyphens.", number);
            }
            Number = number;
        }

        public string Number { get; init; }
        public AreaCodeValueObject AreaCode { get; init; }

        [GeneratedRegex("^[2-9][0-9]{3,4}[0-9]{4}$", RegexOptions.Compiled)]
        private static partial Regex PhoneNumberRegex();
    }
}