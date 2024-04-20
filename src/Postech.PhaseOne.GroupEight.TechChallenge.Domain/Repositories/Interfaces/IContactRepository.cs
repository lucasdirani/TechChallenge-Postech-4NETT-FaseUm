using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts;
using System.Security.Principal;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Repositories.Interfaces;

public interface IContactRepository
{
    Task<ContactEntity> AddContact(AddNewContactHandler addNewContactHandler);
    Task<ContactEntity> FindContactByAreaCode(FindContactByAreaCodeHandler findContactByAreaCode);
    Task<bool> DeleteContactById(DeleteContactHandler deleteContact);
    Task<bool> UpdateContactById(UpdateContactHandler updateContact);
}
