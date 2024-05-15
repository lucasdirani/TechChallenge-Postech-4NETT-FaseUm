using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common
{
    [ExcludeFromCodeCoverage]
    public class DomainException(string message) : Exception(message)
    {
        public static void ThrowWhen(bool invalidRule, string message)
        {
            if (invalidRule)
            {
                throw new DomainException(message);
            }
        }

        public static void ThrowWhenThereAreErrorMessages(IEnumerable<ValidationResult> validationResults)
        {
            if (validationResults.Any())
            {
                throw new DomainException(validationResults.ElementAt(0).ErrorMessage);
            }              
        }
    }
}