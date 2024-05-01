using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts
{
    /// <summary>
    /// Handler for updating a contact.
    /// </summary>
    public class UpdateContactHandler(IContactRepository contactRepository) : IRequestHandler<UpdateContactInput, DefaultOutput>
    {
        /// <summary>
        /// The contact repository.
        /// </summary>
        private readonly IContactRepository _contactRepository = contactRepository;

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
            EntityInactiveException.ThrowWhenIsInactive(contact, "The contact has already been deleted");

            var contactName = new ContactNameValueObject(request.Name, request.LastName);
            var contactEmail = new ContactEmailValueObject(request.Email);
            var contactPhone = new ContactPhoneValueObject(request.Phone, AreaCodeValueObject.Create(request.AreaCode));
            contact.Update(contactName, contactEmail, contactPhone);     

            await _contactRepository.SaveChangesAsync();
            return new DefaultOutput(true, "The contact was successfully updated");
        }
    }
}