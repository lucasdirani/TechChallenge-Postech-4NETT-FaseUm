using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class ContactLastNameException(string message, string lastNameValue) : DomainException(message)
    {
        public string LastNameValue { get; } = lastNameValue;
    }
}