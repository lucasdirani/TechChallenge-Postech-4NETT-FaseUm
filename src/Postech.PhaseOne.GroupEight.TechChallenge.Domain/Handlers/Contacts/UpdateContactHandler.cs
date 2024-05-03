using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Factories.Interfaces;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts
{
    /// <summary>
    /// Handler for updating a contact.
    /// </summary>
    public class UpdateContactHandler(IContactRepository contactRepository, IContactPhoneValueObjectFactory phoneValueObjectFactory) : IRequestHandler<UpdateContactInput, DefaultOutput>
    {
        private readonly IContactRepository _contactRepository = contactRepository;

        private readonly IContactPhoneValueObjectFactory _phoneValueObjectFactory = phoneValueObjectFactory;

        /// <summary>
        /// Handles the update contact request.
        /// </summary>
        /// <param name="request">The update contact request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The default output.</returns>
        public async Task<DefaultOutput> Handle(UpdateContactInput request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetByIdAsync(request.ContactId);
            NotFoundException.ThrowWhenNullEntity(contact, "Contact could not be found");

            var contactName = new ContactNameValueObject(request.Name, request.LastName);
            var contactEmail = new ContactEmailValueObject(request.Email);
            var contactPhone = await _phoneValueObjectFactory.CreateAsync(request.Phone, request.AreaCode);        
            contact.Update(contactName, contactEmail, contactPhone);

            _contactRepository.Update(contact);
            await _contactRepository.SaveChangesAsync();
            return new DefaultOutput(true, "The contact was successfully updated");
        }
    }
}