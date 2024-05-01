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

        [GeneratedRegex("^[A-Za-zÀ-ÖØ-öø-ÿ]+(?:-[A-Za-zÀ-ÖØ-öø-ÿ]+)?(?:\\s[A-Za-zÀ-ÖØ-öø-ÿ]+(?:-[A-Za-zÀ-ÖØ-öø-ÿ]+)?)?$", RegexOptions.Compiled)]
        private static partial Regex FirstNameRegex();

        [GeneratedRegex("^[A-Za-zÀ-ÖØ-öø-ÿ]+(?:-[A-Za-zÀ-ÖØ-öø-ÿ]+)?(?:\\s[A-Za-zÀ-ÖØ-öø-ÿ]+(?:-[A-Za-zÀ-ÖØ-öø-ÿ]+)?)?$", RegexOptions.Compiled)]
        private static partial Regex LastNameRegex();
    }
}