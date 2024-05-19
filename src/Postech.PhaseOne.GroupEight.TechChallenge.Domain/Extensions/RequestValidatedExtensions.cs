using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class RequestValidatedExtensions
    {
        public static IEnumerable<ValidationResult> Validate(this IRequest<DefaultOutput> obj)
        {
            List<ValidationResult> validationResult = [];
            ValidationContext validationContext = new(obj, null, null);
            Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return validationResult;
        }
    }
}