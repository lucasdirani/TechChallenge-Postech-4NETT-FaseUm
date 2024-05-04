﻿using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

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
        /// Updates the contact's name if their first name or last name has changed.
        /// </summary>
        /// <param name="contactFirstName">The contact's new first name.</param>
        /// <param name="contactLastName">The contact's new last name.</param>
        public void UpdateContactName(string contactFirstName, string contactLastName)
        {
            if (ContactName.HasBeenChanged(contactFirstName, contactLastName))
            {
                ContactName = new ContactNameValueObject(contactFirstName, contactLastName);
            }
        }

        /// <summary>
        /// Updates the contact's email if their value has changed.
        /// </summary>
        /// <param name="contactEmail">The contact's new email.</param>
        public void UpdateContactEmail(string contactEmail)
        {
            if (ContactEmail.HasBeenChanged(contactEmail))
            {
                ContactEmail = new ContactEmailValueObject(contactEmail);
            }
        }

        /// <summary>
        /// Updates the contact's phone if their value has changed.
        /// </summary>
        /// <param name="contactPhone">The contact's new phone.</param>
        public void UpdateContactPhone(ContactPhoneValueObject contactPhone)
        {
            if (ContactPhone.HasBeenChanged(contactPhone.Number, contactPhone.AreaCode))
            {
                ContactPhone = contactPhone;
            }
        }
    }
}