namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects
{
    public class AreaCodeValueNotSupportedException : Exception
    {
        public string AreaCodeValue { get; }

        public AreaCodeValueNotSupportedException(string? message, string areaCodeValue) : base(message)
        {
            AreaCodeValue = areaCodeValue;
        }

        public AreaCodeValueNotSupportedException(string? message, string areaCodeValue, Exception? innerException) : base(message, innerException)
        {
            AreaCodeValue = areaCodeValue;
        }
    }
}