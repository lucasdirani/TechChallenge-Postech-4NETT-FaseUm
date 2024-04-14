namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects
{
    public class AreaCodeValueException : Exception
    {
        public string Value { get; }

        public AreaCodeValueException(string? message, string value) : base(message)
        {
            Value = value;
        }

        public AreaCodeValueException(string? message, string value, Exception? innerException) : base(message, innerException)
        {
            Value = value;
        }
    }
}