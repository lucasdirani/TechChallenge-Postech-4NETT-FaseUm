using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class ContactFirstNameException(string message, string firstNameValue) : DomainException(message)
    {
        public string FirstNameValue { get; } = firstNameValue;
    }
}