using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts;
using System.Security.Principal;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Repositories.Interfaces;

public interface IRepository<T> where T : EntityBase
{
    Task<T> AddContact(T entity);
    Task<T> FindContactByAreaCode(T contactEntity);
    Task<bool> DeleteContactById(int id);
    Task<bool> UpdateContactById(T contactEntity);
}
