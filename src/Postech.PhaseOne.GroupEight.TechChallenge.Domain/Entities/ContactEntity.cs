using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities
{
    public class ContactEntity : EntityBase
    {
        public ContactEntity(
            ContactNameValueObject contactName,
            ContactEmailValueObject contactEmail,
            ContactPhoneValueObject contactPhone) 
            : base()
        { 
            ContactName = contactName;
            ContactEmail = contactEmail;
            ContactPhone = contactPhone;
        }

        private ContactEntity() { }

        public ContactNameValueObject ContactName { get; private set; }

        public ContactEmailValueObject ContactEmail { get; private set; }

        public ContactPhoneValueObject ContactPhone { get; private set; }
    }
}