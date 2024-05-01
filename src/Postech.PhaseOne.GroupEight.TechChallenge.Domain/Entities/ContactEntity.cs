using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities
{

    public class ContactEntity(
        ContactNameValueObject contactName,
        ContactEmailValueObject contactEmail,
        ContactPhoneValueObject contactPhone)
        : EntityBase()
    {
        public ContactNameValueObject ContactName { get; private set; } = contactName;

        public ContactEmailValueObject ContactEmail { get; private set; } = contactEmail;

        public ContactPhoneValueObject ContactPhone { get; private set; } = contactPhone;

        /// <summary>
        /// Updates the contact information.
        /// </summary>
        /// <param name="contactName">The new contact name.</param>
        /// <param name="contactEmail">The new contact email.</param>
        /// <param name="contactPhone">The new contact phone.</param>
        public void Update(ContactNameValueObject contactName, ContactEmailValueObject contactEmail, ContactPhoneValueObject contactPhone)
        {
            ContactName = contactName;
            ContactEmail = contactEmail;
            ContactPhone = contactPhone;
        }
    }
}