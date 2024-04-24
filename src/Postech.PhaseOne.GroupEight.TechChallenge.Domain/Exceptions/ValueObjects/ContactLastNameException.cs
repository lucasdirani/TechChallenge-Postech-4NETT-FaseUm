namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects
{
    public class ContactLastNameException(string message, string lastNameValue) : Exception(message)
    {
        public string LastNameValue { get; } = lastNameValue;
    }
}