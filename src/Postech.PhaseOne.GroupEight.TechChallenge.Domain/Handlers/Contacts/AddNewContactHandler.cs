using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Factories.Interfaces;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts
{
    /// <summary>
    /// Handler that manages the registration of a contact.
    /// </summary>
    /// <param name="contactRepository">>Repository that accesses contacts stored in the database.</param>
    /// <param name="contactPhoneFactory">Factory that encapsulates the logic for creating a contact phone number.</param>
    public class AddNewContactHandler(IContactRepository contactRepository, IContactPhoneValueObjectFactory contactPhoneFactory) : IRequestHandler<AddContactInput, DefaultOutput>
    {
        private readonly IContactRepository _contactRepository = contactRepository;
        private readonly IContactPhoneValueObjectFactory _contactPhoneFactory = contactPhoneFactory;

        /// <summary>
        /// Handles the register contact request.
        /// </summary>
        /// <param name="request">Data required to register the contact.</param>
        /// <param name="cancellationToken">Token to cancel the contact register process.</param>
        /// <returns>Set of data indicating whether the contact register operation was performed successfully.</returns>
        public async Task<DefaultOutput> Handle(AddContactInput request, CancellationToken cancellationToken)
        {       
            ContactNameValueObject contactName = new(request.ContactFirstName, request.ContactLastName);
            ContactEmailValueObject contactEmail = new(request.ContactEmail);
            ContactPhoneValueObject contactPhone = await _contactPhoneFactory.CreateAsync(request.ContactPhoneNumber, request.ContactPhoneNumberAreaCode);
            ContactEntity contact = new(contactName, contactEmail, contactPhone);

            bool contactExists = await _contactRepository.ExistsAsync(x => 
                x.ContactEmail.Equals(request.ContactEmail)
                && x.ContactName.Equals(request.ContactFirstName)
                && x.ContactPhone.Number.Equals(request.ContactPhoneNumber)
                && x.ContactPhone.AreaCode.Equals(request.ContactPhoneNumberAreaCode));

            DomainException.ThrowWhen(contactExists, "The contact is already registered.");

            await _contactRepository.InsertAsync(contact);
            await _contactRepository.SaveChangesAsync();
            return new DefaultOutput(true, "The contact was registered successfully.", new { contact });
        }
    }
}