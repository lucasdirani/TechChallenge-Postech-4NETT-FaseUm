using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class AreaCodeValueNotSupportedException(string message, string areaCodeValue) : DomainException(message)
    {
        public string AreaCodeValue { get; } = areaCodeValue;
    }
}