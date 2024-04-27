namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects
{
    public class ContactPhoneNumberException(string message, string phoneNumber) : Exception(message)
    {
        public string PhoneNumber { get; } = phoneNumber;
    }
}