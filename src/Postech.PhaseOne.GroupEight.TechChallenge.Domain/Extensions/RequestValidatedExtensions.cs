using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Extensions
{
    //[ExcludeFromCodeCoverage]
    public static class RequestValidatedExtensions
    {
        public static IEnumerable<ValidationResult> Validadate(this IRequest<DefaultOutput> obj)
        {
            var resultadoValidacao = new List<ValidationResult>();
            var contexto = new ValidationContext(obj, null, null);
            Validator.TryValidateObject(obj, contexto, resultadoValidacao, true);
            return resultadoValidacao;
        }
    }
}