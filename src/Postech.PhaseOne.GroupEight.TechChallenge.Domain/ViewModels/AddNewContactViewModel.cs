using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.ViewModels
{
    /// <summary>
    /// Object that stores data returned from the add contact endpoint.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public record AddNewContactViewModel
    {
        /// <summary>
        /// The identification of the contact.
        /// </summary>
        [JsonPropertyName("contactId")]
        public Guid ContactId { get; init; }

        /// <summary>
        /// The first name of the contact.
        /// </summary>
        [JsonPropertyName("contactFirstName")]
        public string ContactFirstName { get; init; }

        /// <summary>
        /// The last name of the contact.
        /// </summary>
        [JsonPropertyName("contactLastName")]
        public string ContactLastName { get; init; }

        /// <summary>
        /// The email address of the contact.
        /// </summary>
        [JsonPropertyName("contactEmail")]
        public string ContactEmail { get; init; }

        /// <summary>
        /// The phone number of the contact.
        /// </summary>
        [JsonPropertyName("contactPhoneNumber")]
        public string ContactPhoneNumber { get; init; }

        /// <summary>
        /// The area code phone number of the contact.
        /// </summary>
        [JsonPropertyName("contactPhoneAreaCode")]
        public string ContactPhoneAreaCode { get; init; }
    }
}