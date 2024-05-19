using Bogus;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.UnitTests.Fakers.Domain.Entities
{
    internal class ContactEntityFaker : Faker<ContactEntity>
    {
        public ContactEntityFaker(string areaCode = "11")
        {
            Locale = "pt_BR";
            CustomInstantiator(c => new ContactEntity(
                new(c.Name.FirstName(), c.Name.LastName()), 
                new(c.Internet.Email()), 
                new(c.Phone.PhoneNumber("9########"), AreaCodeValueObject.Create(areaCode)))
            );
        }
    }
}