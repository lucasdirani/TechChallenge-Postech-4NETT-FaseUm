using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts;
using System.Security.Principal;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Repositories.Interfaces;

public interface IContactRepository
{
    Task<ContactEntity> AddContact(ContactEntity contactEntity);
    Task<ContactEntity> FindContactByAreaCode(ContactEntity contactEntity);
    Task<bool> DeleteContactById(int id);
    Task<bool> UpdateContactById(ContactEntity contactEntity);
}
