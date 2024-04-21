using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities
{
    public class ContactEntity(
        ContactNameValueObject contactName,
        ContactEmailValueObject contactEmail,
        ContactPhoneValueObject contactPhone) 
        : EntityBase()
    {
        public ContactNameValueObject ContactName { get; private set; } = contactName;

        public ContactEmailValueObject ContactEmail { get; private set; } = contactEmail;

        public ContactPhoneValueObject ContactPhone { get; private set; } = contactPhone;
    }
}