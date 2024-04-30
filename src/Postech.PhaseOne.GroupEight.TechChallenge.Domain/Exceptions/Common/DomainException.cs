using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common
{
    [ExcludeFromCodeCoverage]
    public class DomainException(string message) : Exception(message)
    {
        public static void ThrowWhen(bool invalidRule, string message)
        {
            if (invalidRule) throw new DomainException(message);
        }
    }
}