using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Repositories.Interfaces;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Infra.Repositories;

public class ContactRepository : IContactRepository
{
    public Task<ContactEntity> AddContact(AddNewContactHandler addNewContactHandler)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteContactById(DeleteContactHandler deleteContact)
    {
        throw new NotImplementedException();
    }

    public Task<ContactEntity> FindContactByAreaCode(FindContactByAreaCodeHandler findContactByAreaCode)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateContactById(UpdateContactHandler updateContact)
    {
        throw new NotImplementedException();
    }
}
