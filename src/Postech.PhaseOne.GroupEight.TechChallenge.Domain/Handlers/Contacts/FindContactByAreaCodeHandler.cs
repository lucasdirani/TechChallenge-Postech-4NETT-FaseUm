using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;
using System.Linq.Expressions;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts
{
    public class FindContactByAreaCodeHandler : IRequestHandler<FindContactInput, ContactListOutput>
    {
        private readonly IContactRepository _contactRepository;

        public FindContactByAreaCodeHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<ContactListOutput> Handle(FindContactInput request, CancellationToken cancellationToken)
        {
            Expression<Func<ContactEntity, bool>> expression = contact =>
                   contact.ContactPhone.AreaCode.Value == request.AreaCodeValue;

            List<ContactEntity> contactsList = await _contactRepository.FindAsync(expression);

            return new ContactListOutput(true, contactsList);
        }
    }
}