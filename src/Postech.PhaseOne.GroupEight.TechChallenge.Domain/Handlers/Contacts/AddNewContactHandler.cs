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
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ViewModels;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts
{
    /// <summary>
    /// Handler that manages the registration of a contact.
    /// </summary>
    /// <param name="contactRepository">>Repository that accesses contacts stored in the database.</param>
    /// <param name="contactPhoneFactory">Factory that encapsulates the logic for creating a contact phone number.</param>
    /// <param name="registeredContactChecker">Encapsulates the logic to check the existing contact record.</param>
    public class AddNewContactHandler(
        IContactRepository contactRepository, 
        IContactPhoneValueObjectFactory contactPhoneFactory,
        [FromKeyedServices(nameof(AddNewContactChecker))] IRegisteredContactChecker registeredContactChecker) 
        : IRequestHandler<AddContactInput, DefaultOutput<AddNewContactViewModel>>
    {
        private readonly IContactRepository _contactRepository = contactRepository;
        private readonly IContactPhoneValueObjectFactory _contactPhoneFactory = contactPhoneFactory;
        private readonly IRegisteredContactChecker _registeredContactChecker = registeredContactChecker;

        /// <summary>
        /// Handles the register contact request.
        /// </summary>
        /// <param name="request">Data required to register the contact.</param>
        /// <param name="cancellationToken">Token to cancel the contact register process.</param>
        /// <returns>Set of data indicating whether the contact register operation was performed successfully.</returns>
        /// <exception cref="ContactFirstNameException">The first name provided for the contact is in an invalid format.</exception>
        /// <exception cref="ContactLastNameException">The last name provided for the contact is in an invalid format.</exception>
        /// <exception cref="ContactEmailAddressException">The email address provided for the contact is in an invalid format.</exception>
        /// <exception cref="ContactPhoneNumberException">The phone number provided for the contact is in an invalid format.</exception>
        /// <exception cref="DomainException">The contact is already registered.</exception>
        /// <exception cref="NotFoundException">The provided area code was not found.</exception>
        public async Task<DefaultOutput<AddNewContactViewModel>> Handle(AddContactInput request, CancellationToken cancellationToken)
        {       
            ContactNameValueObject contactName = new(request.ContactFirstName, request.ContactLastName);
            ContactEmailValueObject contactEmail = new(request.ContactEmail);
            ContactPhoneValueObject contactPhone = await _contactPhoneFactory.CreateAsync(request.ContactPhoneNumber, request.ContactPhoneNumberAreaCode);
            ContactEntity contact = new(contactName, contactEmail, contactPhone);
            bool isContactRegistered = await _registeredContactChecker.CheckRegisteredContactAsync(contact);
            DomainException.ThrowWhen(isContactRegistered, "The contact is already registered.");
            await _contactRepository.InsertAsync(contact);
            await _contactRepository.SaveChangesAsync();
            return new DefaultOutput<AddNewContactViewModel>(true, "The contact was registered successfully.", contact.AsAddNewContactViewModel());
        }
    }
}