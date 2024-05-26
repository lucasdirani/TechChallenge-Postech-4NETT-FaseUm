using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Checkers;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Checkers.Interfaces;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Extensions;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Factories.Interfaces;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ViewModels;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts
{
    /// <summary>
    /// Handler that manages the update of a contact's data.
    /// </summary>
    /// <param name="contactRepository">Repository that accesses contacts stored in the database.</param>
    /// <param name="contactPhoneFactory">Factory that encapsulates the logic for creating a contact phone number.</param>
    /// <param name="registeredContactChecker">Encapsulates the logic to check the existing contact record.</param>
    public class UpdateContactHandler(
        IContactRepository contactRepository, 
        IContactPhoneValueObjectFactory contactPhoneFactory,
        [FromKeyedServices(nameof(UpdateContactChecker))] IRegisteredContactChecker registeredContactChecker) 
        : IRequestHandler<UpdateContactInput, DefaultOutput<UpdateContactViewModel>>
    {
        private readonly IContactRepository _contactRepository = contactRepository;
        private readonly IContactPhoneValueObjectFactory _contactPhoneFactory = contactPhoneFactory;
        private readonly IRegisteredContactChecker _registeredContactChecker = registeredContactChecker;

        /// <summary>
        /// Handles the update contact request.
        /// </summary>
        /// <param name="request">The contact's update data.</param>
        /// <param name="cancellationToken">Token to cancel the contact details update process.</param>
        /// <returns>Set of data indicating whether the contact update operation was performed successfully.</returns>
        /// <exception cref="NotFoundException">The contact provided for the update was not found or the area code for the contact's new phone number was not found.</exception>
        /// <exception cref="DomainException">The contact is already registered.</exception>
        /// <exception cref="ContactFirstNameException">The new first name provided for the contact is in an invalid format.</exception>
        /// <exception cref="ContactLastNameException">The new last name provided for the contact is in an invalid format.</exception>
        /// <exception cref="ContactEmailAddressException">The new email address provided for the contact is in an invalid format.</exception>
        /// <exception cref="ContactPhoneNumberException">The new phone number provided for the contact is in an invalid format.</exception>
        public async Task<DefaultOutput<UpdateContactViewModel>> Handle(UpdateContactInput request, CancellationToken cancellationToken)
        {
            ContactEntity? contact = await _contactRepository.GetByIdAsync(request.ContactId);
            NotFoundException.ThrowWhenNullEntity(contact, "Contact could not be found");
            contact.UpdateContactName(request.ContactFirstName, request.ContactLastName);
            contact.UpdateContactEmail(request.ContactEmail);
            contact.UpdateContactPhone(await _contactPhoneFactory.CreateAsync(request.ContactPhoneNumber, request.ContactPhoneNumberAreaCode));
            contact.UpdateContactActiveStatus(request.IsActive);
            bool isContactRegistered = await _registeredContactChecker.CheckRegisteredContactAsync(contact);
            DomainException.ThrowWhen(isContactRegistered, "Contact already registered.");
            _contactRepository.Update(contact);
            await _contactRepository.SaveChangesAsync();
            return new DefaultOutput<UpdateContactViewModel>(true, "The contact was successfully updated", contact.AsUpdateContactViewModel());
        }
    }
}