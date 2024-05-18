using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts;

public class DeleteContactHandler(IContactRepository contactRepository) : IRequestHandler<DeleteContactInput, DefaultOutput>
{
    private readonly IContactRepository _contactRepository = contactRepository;

    public async Task<DefaultOutput> Handle(DeleteContactInput request, CancellationToken cancellationToken)
    {
        ContactEntity? contactToBeDeleted = await _contactRepository.GetByIdAsync(request.ContactId);
        NotFoundException.ThrowWhenNullEntity(contactToBeDeleted, "Contact could not be found");
        contactToBeDeleted.Inactivate();
        await _contactRepository.SaveChangesAsync();
        return new DefaultOutput(true, "The contact was successfully deleted");
    }
}