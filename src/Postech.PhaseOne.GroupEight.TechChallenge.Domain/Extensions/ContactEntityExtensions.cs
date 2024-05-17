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
                ContactEmail = contact.ContactEmail.Value,
                ContactFirstName = contact.ContactName.FirstName,
                ContactLastName = contact.ContactName.LastName,
                ContactPhoneAreaCode = contact.ContactPhone.AreaCode.Value,
                ContactPhoneNumber = contact.ContactPhone.Number
            });
        }
    }
}