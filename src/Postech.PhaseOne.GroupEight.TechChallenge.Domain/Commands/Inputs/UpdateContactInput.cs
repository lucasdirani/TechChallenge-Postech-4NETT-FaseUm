using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs
{
    /// <summary>
    /// Object that stores the update data for a contact.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public record UpdateContactInput : IRequest<DefaultOutput<UpdateContactViewModel>>
    {
        /// <summary>
        /// The unique identifier of the contact that will be updated.
        /// </summary>
        [Required(ErrorMessage = "The contact identifier is required.")]
        public Guid ContactId { get; init; }

        /// <summary>
        /// The new first name of the contact.
        /// </summary>
        [Required(ErrorMessage = "The first name is required.")]
        [MaxLength(40, ErrorMessage = "The first name must contain a maximum of 40 characters.")]
        public string ContactFirstName { get; init; }

        /// <summary>
        /// The new last name of the contact.
        /// </summary>
        [Required(ErrorMessage = "The last name is required.")]
        [MaxLength(60, ErrorMessage = "The last name must contain a maximum of 60 characters.")]
        public string ContactLastName { get; init; }

        /// <summary>
        /// The new email address of the contact.
        /// </summary>
        [Required(ErrorMessage = "The email address is required.")]
        [MaxLength(60, ErrorMessage = "The email address must contain a maximum of 60 characters.")]
        public string ContactEmail { get; init; }

        /// <summary>
        /// The new phone number of the contact.
        /// </summary>
        [Required(ErrorMessage = "The phone number is required.")]
        [MaxLength(9, ErrorMessage = "The phone number must contain a maximum of 9 characters.")]
        public string ContactPhoneNumber { get; init; }

        /// <summary>
        /// The new area code phone number of the contact.
        /// </summary>
        [Required(ErrorMessage = "The area code is required.")]
        [RegularExpression("^\\d{2}$", ErrorMessage = "The area code must contain 2 numeric characters.")]
        public string ContactPhoneNumberAreaCode { get; init; }

        /// <summary>
        /// Indicates whether the contact is active or not.
        /// </summary>
        public bool IsActive { get; set; }
    }
}