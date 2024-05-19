using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories
{
    public interface IContactRepository : IRepository<ContactEntity, Guid>
    {
        Task<IEnumerable<ContactEntity>> GetContactsByAreaCodeValueAsync(string areaCodeValue);
        Task<AreaCodeValueObject?> GetAreaCodeByValueAsync(string areaCodeValue);
        Task<ContactPhoneValueObject?> GetContactPhoneByNumberAndAreaCodeValueAsync(string phoneNumber, string areaCodeValue);
        Task<IEnumerable<ContactEntity>> GetContactsByContactPhoneAsync(ContactPhoneValueObject contactPhone);
    }
}