using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects;
using System.Text.RegularExpressions;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects
{
    public partial record ContactEmailValueObject
    {
        public ContactEmailValueObject(string value)
        {
            ContactEmailAddressException.ThrowIfFormatIsInvalid(value, EmailValueRegex());
            Value = value;
        }

        public string Value { get; init; }

        [GeneratedRegex("^[\\w-]+(?:\\.[\\w-]+)*@(?:[\\w-]+\\.)+[a-zA-Z]{2,7}$", RegexOptions.Compiled)]
        private static partial Regex EmailValueRegex();
    }
}