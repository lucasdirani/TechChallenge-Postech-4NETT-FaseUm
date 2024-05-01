using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs
{
    [ExcludeFromCodeCoverage]
    public record DeleteContactInput : IRequest<DefaultOutput>
    {
        public Guid ContactId { get; init; }
    }
}