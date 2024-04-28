using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class ContactEmailAddressException(string message, string emailAddressValue) : DomainException(message)
    {
        public string EmailAddressValue { get; } = emailAddressValue;
    }
}