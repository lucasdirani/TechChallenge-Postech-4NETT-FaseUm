using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Factories.Interfaces;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts
{
    /// <summary>
    /// Handler that manages the update of a contact's data.
    /// </summary>
    public class UpdateContactHandler(IContactRepository contactRepository, IContactPhoneValueObjectFactory contactPhoneFactory) : IRequestHandler<UpdateContactInput, DefaultOutput>
    {
        private readonly IContactRepository _contactRepository = contactRepository;
        private readonly IContactPhoneValueObjectFactory _contactPhoneFactory = contactPhoneFactory;

        /// <summary>
        /// Handles the update contact request.
        /// </summary>
        /// <param name="request">The contact's update data.</param>
        /// <param name="cancellationToken">Token to cancel the contact details update process.</param>
        /// <returns>Set of data indicating whether the contact update operation was performed successfully.</returns>
        /// <exception cref="NotFoundException">The contact provided for the update was not found or the area code for the contact's new phone number was not found.</exception>
        /// <exception cref="ContactFirstNameException">The new first name provided for the contact is in an invalid format.</exception>
        /// <exception cref="ContactLastNameException">The new last name provided for the contact is in an invalid format.</exception>
        /// <exception cref="ContactEmailAddressException">The new email address provided for the contact is in an invalid format.</exception>
        public async Task<DefaultOutput> Handle(UpdateContactInput request, CancellationToken cancellationToken)
        {
            using(var transaction = await _contactRepository.BeginTransactionAsync())
            {
                try
                {
                    ContactEntity? contact = await _contactRepository.GetByIdAsync(request.ContactId);
                    NotFoundException.ThrowWhenNullEntity(contact, "Contact could not be found");

                    bool contactExists = await _contactRepository.ExistsAsync(x => x.Id != request.ContactId
                        && x.ContactEmail.Value.Equals(request.ContactEmail)
                        && x.ContactName.FirstName.Equals(request.ContactFirstName)
                        && x.ContactName.LastName.Equals(request.ContactLastName)
                        && x.ContactPhone.Number.Equals(request.ContactPhoneNumber)
                        && x.ContactPhone.AreaCode.Value.Equals(request.ContactPhoneNumberAreaCode));
                    DomainException.ThrowWhen(contactExists, "Contact already registered.");

                    contact.UpdateContactName(request.ContactFirstName, request.ContactLastName);
                    contact.UpdateContactEmail(request.ContactEmail);
                    contact.UpdateContactPhone(await _contactPhoneFactory.CreateAsync(request.ContactPhoneNumber, request.ContactPhoneNumberAreaCode));
                    contact.UpdateContactActiveStatus(request.IsActive);

                    _contactRepository.Update(contact);
                    await _contactRepository.SaveChangesAsync();

                    transaction.Commit();
                    return new DefaultOutput(true, "The contact was successfully updated", contact);
                    
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }             
        }
    }
}