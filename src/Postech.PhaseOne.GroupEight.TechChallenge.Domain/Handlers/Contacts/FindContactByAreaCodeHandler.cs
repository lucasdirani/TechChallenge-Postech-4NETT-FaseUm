using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ViewModels;
using System.Linq.Expressions;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts
{
    public class FindContactByAreaCodeHandler(IContactRepository contactRepository) : IRequestHandler<FindContactInput, DefaultOutput>
    {
        private readonly IContactRepository _contactRepository = contactRepository;

        public async Task<DefaultOutput> Handle(FindContactInput request, CancellationToken cancellationToken)
        {
            Expression<Func<ContactEntity, bool>> expression = contact =>
                   contact.ContactPhone.AreaCode.Value == request.AreaCodeValue && contact.Active;
            List<ContactEntity> contacts = await _contactRepository.FindAsync(expression);
            return new DefaultOutput(true, contacts.Select(contact => new FindContactByAreaCodeViewModel()
            {
                ContactEmail = contact.ContactEmail.Value,
                ContactFirstName = contact.ContactName.FirstName,
                ContactLastName = contact.ContactName.LastName,
                ContactPhoneAreaCode = contact.ContactPhone.AreaCode.Value,
                ContactPhoneNumber = contact.ContactPhone.Number
            }));
        }
    }
}