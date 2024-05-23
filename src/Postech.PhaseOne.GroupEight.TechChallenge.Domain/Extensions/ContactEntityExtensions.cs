using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ViewModels;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ContactEntityExtensions
    {
        public static IEnumerable<FindContactByAreaCodeViewModel> AsFindContactByAreaCodeViewModel(this IEnumerable<ContactEntity> contacts)
        {
            return contacts.Select(contact => new FindContactByAreaCodeViewModel()
            {
                ContactId = contact.Id,
                ContactEmail = contact.ContactEmail.Value,
                ContactFirstName = contact.ContactName.FirstName,
                ContactLastName = contact.ContactName.LastName,
                ContactPhoneAreaCode = contact.ContactPhone.AreaCode.Value,
                ContactPhoneNumber = contact.ContactPhone.Number
            });
        }

        public static AddNewContactViewModel AsAddNewContactViewModel(this ContactEntity contact)
        {
            return new AddNewContactViewModel()
            {
                ContactId = contact.Id,
                ContactEmail = contact.ContactEmail.Value,
                ContactFirstName = contact.ContactName.FirstName,
                ContactLastName = contact.ContactName.LastName,
                ContactPhoneAreaCode = contact.ContactPhone.AreaCode.Value,
                ContactPhoneNumber = contact.ContactPhone.Number
            };
        }

        public static UpdateContactViewModel AsUpdateContactViewModel(this ContactEntity contact)
        {
            return new UpdateContactViewModel()
            {
                ContactId = contact.Id,
                ContactEmail = contact.ContactEmail.Value,
                ContactFirstName = contact.ContactName.FirstName,
                ContactLastName = contact.ContactName.LastName,
                ContactPhoneAreaCode = contact.ContactPhone.AreaCode.Value,
                ContactPhoneNumber = contact.ContactPhone.Number,
                IsActive = contact.IsActive()
            };
        }

        public static ContactEntity Copy(this ContactEntity contact)
        {
            return new(contact.ContactName, contact.ContactEmail, contact.ContactPhone);
        }
    }
}