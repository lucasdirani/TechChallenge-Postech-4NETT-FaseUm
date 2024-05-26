using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs
{
    /// <summary>
    /// Object that stores the register data for a contact.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AddContactInput : IRequest<DefaultOutput<AddNewContactViewModel>>
    {
        /// <summary>
        /// The first name of the contact.
        /// </summary>
        [Required(ErrorMessage = "Contact first name is required.")]
        [MaxLength(40, ErrorMessage = "The contact first name must contain a maximum of 40 characters.")]
        public string ContactFirstName { get; set; }

        /// <summary>
        /// The last name of the contact.
        /// </summary>
        [Required(ErrorMessage = "Contact last name is required.")]
        [MaxLength(60, ErrorMessage = "The contact last name must contain a maximum of 60 characters.")]
        public string ContactLastName { get; set; }

        /// <summary>
        /// The email address of the contact.
        /// </summary>
        [Required(ErrorMessage = "Contact email address is required.")]
        [MaxLength(60, ErrorMessage = "The contact email address must contain a maximum of 60 characters.")]
        public string ContactEmail { get; set; }

        /// <summary>
        /// The phone number of the contact.
        /// </summary>
        [Required(ErrorMessage = "Contact phone number is required.")]        
        [MaxLength(9, ErrorMessage = "The contact phone number must contain a maximum of 9 characters.")]
        public string ContactPhoneNumber { get; set; }

        /// <summary>
        /// The area code phone number of the contact.
        /// </summary>
        [Required(ErrorMessage = "Contact area code phone number is required.")]
        [RegularExpression("^\\d{2}$", ErrorMessage = "The area code must contain 2 numeric characters.")]
        public string ContactPhoneNumberAreaCode { get; set; }
    }
}