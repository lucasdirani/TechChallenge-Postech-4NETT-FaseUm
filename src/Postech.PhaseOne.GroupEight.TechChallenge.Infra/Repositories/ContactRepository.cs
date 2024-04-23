using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Repositories.Interfaces;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Infra.Repositories;

public class ContactRepository : IContactRepository
{
    public async Task<ContactEntity> AddContact(ContactEntity contactEntity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteContactById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ContactEntity> FindContactByAreaCode(ContactEntity contactEntity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateContactById(ContactEntity contactEntity)
    {
        throw new NotImplementedException();
    }
}
