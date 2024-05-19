using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts;

/// <summary>
/// Handler that manages the deletion of an existing contact.
/// </summary>
/// <param name="contactRepository">Repository that accesses contacts stored in the database.</param>
public class DeleteContactHandler(IContactRepository contactRepository) : IRequestHandler<DeleteContactInput, DefaultOutput>
{
    private readonly IContactRepository _contactRepository = contactRepository;

    /// <summary>
    /// Handles the delete contact request.
    /// </summary>
    /// <param name="request">Data required to delete the contact.</param>
    /// <param name="cancellationToken">Token to cancel the contact delete process.</param>
    /// <returns>Set of data indicating whether the contact delete operation was performed successfully.</returns>
    /// <exception cref="NotFoundException">The contact provided for the delete was not found.</exception>
    /// <exception cref="EntityInactiveException">The contact has already been inactivated.</exception>
    public async Task<DefaultOutput> Handle(DeleteContactInput request, CancellationToken cancellationToken)
    {
        ContactEntity? contactToBeDeleted = await _contactRepository.GetByIdAsync(request.ContactId);
        NotFoundException.ThrowWhenNullEntity(contactToBeDeleted, "Contact could not be found");
        contactToBeDeleted.Inactivate();
        await _contactRepository.SaveChangesAsync();
        return new DefaultOutput(true, "The contact was successfully deleted");
    }
}