using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects;
using System.Text.RegularExpressions;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects
{
    public partial record ContactNameValueObject
    {
        public ContactNameValueObject(string firstName, string lastName)
        {
            if (!FirstNameRegex().IsMatch(firstName))
            {
                throw new ContactFirstNameException("The contact's first name must only contain letters (lowercase or capital letters), accents and hyphens.", firstName);
            }
            if (!LastNameRegex().IsMatch(lastName))
            {
                throw new ContactLastNameException("The contact's last name must only contain letters (lowercase or capital letters), accents and hyphens.", lastName);
            }
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; init; }
        public string LastName { get; init; }

        [GeneratedRegex("^[A-Za-zÀ-ÖØ-öø-ÿ]+(?:-[A-Za-zÀ-ÖØ-öø-ÿ]+)?$", RegexOptions.Compiled)]
        private static partial Regex FirstNameRegex();

        [GeneratedRegex("^[A-Za-zÀ-ÖØ-öø-ÿ]+(?:-[A-Za-zÀ-ÖØ-öø-ÿ]+)?(?:\\s[A-Za-zÀ-ÖØ-öø-ÿ]+(?:-[A-Za-zÀ-ÖØ-öø-ÿ]+)?)?$", RegexOptions.Compiled)]
        private static partial Regex LastNameRegex();
    }
}