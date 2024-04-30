using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories
{
    public interface IContactRepository : IRepository<ContactEntity, Guid>
    {
        Task<AreaCodeValueObject?> GetAreaCodeByValueAsync(string areaCodeValue);
        Task<ContactPhoneValueObject?> GetContactPhoneByNumberAndAreaCodeValueAsync(string phoneNumber, string areaCodeValue);
    }
}