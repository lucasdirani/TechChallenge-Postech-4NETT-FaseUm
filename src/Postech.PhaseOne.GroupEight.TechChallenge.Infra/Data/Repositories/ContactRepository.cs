using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Infra.Data.Repositories;

public class ContactRepository : IContactRepository
{
    public Task<ContactEntity> AddContact(ContactEntity contactEntity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteContactById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ContactEntity> FindContactByAreaCode(ContactEntity contactEntity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateContactById(ContactEntity contactEntity)
    {
        throw new NotImplementedException();
    }
}