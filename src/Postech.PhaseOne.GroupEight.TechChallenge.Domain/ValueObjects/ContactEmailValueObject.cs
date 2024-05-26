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

        /// <summary>
        /// Indicates whether the email address has a different value.
        /// </summary>
        /// <param name="otherEmailAddress">The new email address that will be used as a comparison for the current email address.</param>
        /// <returns>Returns true if the email address has been changed. Otherwise, it returns false.</returns>
        public bool HasBeenChanged(string otherEmailAddress)
        {
            return Value != otherEmailAddress;
        }

        public virtual bool Equals(ContactEmailValueObject? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }
}