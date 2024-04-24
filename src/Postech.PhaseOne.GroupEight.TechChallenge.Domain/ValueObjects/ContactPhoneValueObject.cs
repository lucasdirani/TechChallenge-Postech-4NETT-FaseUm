using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects;
using System.Text.RegularExpressions;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects
{
    public partial record ContactPhoneValueObject
    {
        public ContactPhoneValueObject(string phoneNumber, AreaCodeValueObject areaCode)
        {
            if (!PhoneNumberRegex().IsMatch(phoneNumber))
            {
                throw new ContactPhoneNumberException("The phone number must only have eight or nine digits, and must not contain special characters, such as hyphens.", phoneNumber);
            }
            Number = phoneNumber;
            AreaCode = areaCode;
        }

        public string Number { get; init; }
        public AreaCodeValueObject AreaCode { get; init; }

        [GeneratedRegex("^[2-9][0-9]{3,4}[0-9]{4}$", RegexOptions.Compiled)]
        private static partial Regex PhoneNumberRegex();
    }
}