using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs
{
    /// <summary>
    /// Object that stores the delete data for a contact.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public record DeleteContactInput : IRequest<DefaultOutput>
    {
        /// <summary>
        /// The unique identifier of the contact that will be deleted.
        /// </summary>
        [Required(ErrorMessage = "Contact identification is required.")]
        public Guid ContactId { get; init; }
    }
}