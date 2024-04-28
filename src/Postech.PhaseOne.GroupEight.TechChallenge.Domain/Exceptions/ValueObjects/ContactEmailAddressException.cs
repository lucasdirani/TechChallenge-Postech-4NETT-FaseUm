namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects
{
    public class ContactEmailAddressException(string message, string emailAddressValue) : Exception(message)
    {
        public string EmailAddressValue { get; } = emailAddressValue;
    }
}