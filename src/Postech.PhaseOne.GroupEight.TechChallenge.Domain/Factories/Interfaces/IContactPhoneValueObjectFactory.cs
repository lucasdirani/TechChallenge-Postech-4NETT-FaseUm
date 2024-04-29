using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Factories.Interfaces
{
    public interface IContactPhoneValueObjectFactory
    {
        Task<ContactPhoneValueObject> CreateAsync(string phoneNumber, string areaCodeValue);
    }
}