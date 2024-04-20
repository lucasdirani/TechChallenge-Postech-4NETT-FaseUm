namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects
{
    public class ContactFirstNameException(string message, string firstNameValue) : Exception(message)
    {
        public string FirstNameValue { get; } = firstNameValue;
    }
}