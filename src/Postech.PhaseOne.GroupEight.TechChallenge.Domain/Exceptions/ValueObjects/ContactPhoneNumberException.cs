using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class ContactPhoneNumberException(string message, string phoneNumber) : DomainException(message)
    {
        public string PhoneNumber { get; } = phoneNumber;
    }
}