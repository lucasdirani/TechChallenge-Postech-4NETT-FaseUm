using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ViewModels;
using System.Linq.Expressions;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts
{
    /// <summary>
    /// Returns list of contacts by area code.
    /// </summary>
    public class FindContactByAreaCodeHandler(IContactRepository contactRepository) : IRequestHandler<FindContactInput, DefaultOutput>
    {
        private readonly IContactRepository _contactRepository = contactRepository;

        /// <summary>
        /// Handles the find contacts request.
        /// </summary>
        /// <param name="request">Request used as the search filter (area code).</param>
        /// <param name="cancellationToken">Token to cancel the contacts search process.</param>
        /// <returns>List of contacts that have the same area code as the request.</returns>
        public async Task<DefaultOutput> Handle(FindContactInput request, CancellationToken cancellationToken)
        {
            Expression<Func<ContactEntity, bool>> expression = contact =>
                   (request.AreaCodeValue == "" || contact.ContactPhone.AreaCode.Value == request.AreaCodeValue) && contact.Active;
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