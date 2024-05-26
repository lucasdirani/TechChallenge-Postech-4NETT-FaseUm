using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class ContactFirstNameException(string message, string firstNameValue) : DomainException(message)
    {
        public string FirstNameValue { get; } = firstNameValue;

        public static void ThrowIfFormatIsInvalid(string argument, Regex format)
        {
            if (!format.IsMatch(argument))
            {
                throw new ContactFirstNameException("The contact's first name must contain only letters (lowercase or uppercase), accents, hyphens and must not exceed forty characters.", argument);
            }
        }
    }
}