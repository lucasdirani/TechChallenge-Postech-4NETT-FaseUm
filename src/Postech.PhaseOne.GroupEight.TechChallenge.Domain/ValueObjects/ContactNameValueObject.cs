using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects;
using System.Text.RegularExpressions;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects
{
    public partial record ContactNameValueObject
    {
        public ContactNameValueObject(string firstName, string lastName)
        {
            ContactFirstNameException.ThrowIfFormatIsInvalid(firstName, FirstNameRegex());
            ContactLastNameException.ThrowIfFormatIsInvalid(lastName, LastNameRegex());
            FirstName = firstName;
            LastName = lastName;
        }
        
        public string FirstName { get; init; }
        public string LastName { get; init; }

        [GeneratedRegex("^(?=.{1,40}$)[A-Za-zÀ-ÖØ-öø-ÿ']+(?:-[A-Za-zÀ-ÖØ-öø-ÿ']+)?(?:\\s[A-Za-zÀ-ÖØ-öø-ÿ']+(?:-[A-Za-zÀ-ÖØ-öø-ÿ']+)?)*$", RegexOptions.Compiled)]
        private static partial Regex FirstNameRegex();

        [GeneratedRegex("^(?=.{1,60}$)[A-Za-zÀ-ÖØ-öø-ÿ']+(?:-[A-Za-zÀ-ÖØ-öø-ÿ']+)?(?:\\s[A-Za-zÀ-ÖØ-öø-ÿ']+(?:-[A-Za-zÀ-ÖØ-öø-ÿ']+)?)*$", RegexOptions.Compiled)]
        private static partial Regex LastNameRegex();

        /// <summary>
        /// Indicates whether the first name or last name has different values.
        /// </summary>
        /// <param name="otherFirstName">The new first name that will be used as a comparison for the current first name.</param>
        /// <param name="otherLastName">The new last name that will be used as a comparison for the current last name.</param>
        /// <returns>Returns true if the first name or last name has been changed. Otherwise, it returns false.</returns>
        public bool HasBeenChanged(string otherFirstName, string otherLastName)
        {
            return FirstName != otherFirstName || LastName != otherLastName;
        }

        public virtual bool Equals(ContactNameValueObject? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(FirstName, other.FirstName, StringComparison.OrdinalIgnoreCase)
                && string.Equals(LastName, other.LastName, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName);
        }
    }
}