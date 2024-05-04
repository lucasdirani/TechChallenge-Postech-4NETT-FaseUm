using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs
{
    /// <summary>
    /// Object that stores the update data for a contact.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public record UpdateContactInput : IRequest<DefaultOutput>
    {
        /// <summary>
        /// The unique identifier of the contact that will be updated.
        /// </summary>
        public Guid ContactId { get; init; }

        /// <summary>
        /// The new first name of the contact.
        /// </summary>
        public string ContactFirstName { get; init; }

        /// <summary>
        /// The new last name of the contact.
        /// </summary>
        public string ContactLastName { get; init; }

        /// <summary>
        /// The new email address of the contact.
        /// </summary>
        public string ContactEmail { get; init; }

        /// <summary>
        /// The new phone number of the contact.
        /// </summary>
        public string ContactPhoneNumber { get; init; }

        /// <summary>
        /// The new area code phone number of the contact.
        /// </summary>
        public string ContactPhoneNumberAreaCode { get; init; }
    }
}