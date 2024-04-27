using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects;
using System.Text.RegularExpressions;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects
{
    public partial record ContactEmailValueObject
    {
        public ContactEmailValueObject(string value)
        {
            if (!EmailValueRegex().IsMatch(value))
            {
                throw new ContactEmailAddressException("The email address is not in a valid format.", value);
            }
            Value = value;
        }

        public string Value { get; init; }

        [GeneratedRegex("^[\\w-]+(?:\\.[\\w-]+)*@(?:[\\w-]+\\.)+[a-zA-Z]{2,7}$", RegexOptions.Compiled)]
        private static partial Regex EmailValueRegex();
    }
}