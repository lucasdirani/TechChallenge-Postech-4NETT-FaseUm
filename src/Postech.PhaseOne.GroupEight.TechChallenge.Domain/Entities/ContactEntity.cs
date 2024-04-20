using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities
{
    public class ContactEntity : EntityBase
    {             
        public ContactNameValueObject? ContactName { get; set; }

        public EmailValueObject? ContactEmail { get; set; }

        public PhoneValueObject? ContactPhone { get; set; }
    }
}