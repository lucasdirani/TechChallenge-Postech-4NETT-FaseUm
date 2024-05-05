using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Factories.Interfaces;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Factories
{
    public class ContactPhoneValueObjectFactory(IContactRepository contactRepository) : IContactPhoneValueObjectFactory
    {
        private readonly IContactRepository _contactRepository = contactRepository;

        public async Task<ContactPhoneValueObject> CreateAsync(string phoneNumber, string areaCodeValue)
        {
            ContactPhoneValueObject? contactPhone = await _contactRepository.GetContactPhoneByNumberAndAreaCodeValueAsync(phoneNumber, areaCodeValue);
            if (contactPhone is not null)
            {
                return contactPhone;
            }
            AreaCodeValueObject? areaCode = await _contactRepository.GetAreaCodeByValueAsync(areaCodeValue);
            NotFoundException.ThrowWhenNullEntity(areaCode, "The area code was not found.");
            return new(phoneNumber, areaCode);
        }
    }
}