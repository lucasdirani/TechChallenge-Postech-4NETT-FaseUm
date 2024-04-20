using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities
{
    public class ContactEntity : EntityBase
    {             
        public ContactNameValueObject? Name { get; set; }

        public EmailValueObject? Email { get; set; }

        public PhoneValueObject? Phone { get; set; }
        
        public List<AddressValueObject>? Addresses { get; set; }
    }
}