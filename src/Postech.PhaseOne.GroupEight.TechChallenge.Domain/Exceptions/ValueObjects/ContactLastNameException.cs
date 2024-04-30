using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class ContactLastNameException(string message, string lastNameValue) : DomainException(message)
    {
        public string LastNameValue { get; } = lastNameValue;

        public static void ThrowIfFormatIsInvalid(string argument, Regex format)
        {
            if (!format.IsMatch(argument))
            {
                throw new ContactLastNameException("The contact's last name must only contain letters (lowercase or capital letters), accents and hyphens.", argument);
            }
        }
    }
}