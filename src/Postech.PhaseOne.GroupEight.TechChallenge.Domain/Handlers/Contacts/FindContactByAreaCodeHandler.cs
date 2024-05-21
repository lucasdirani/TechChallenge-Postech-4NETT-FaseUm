using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Extensions;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ViewModels;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts
{
    /// <summary>
    /// Handler that manages the search for contacts by area code.
    /// </summary>
    /// <param name="contactRepository">Repository that accesses contacts stored in the database.</param>
    public class FindContactByAreaCodeHandler(IContactRepository contactRepository) 
        : IRequestHandler<FindContactInput, DefaultOutput<IEnumerable<FindContactByAreaCodeViewModel>>>
    {
        private readonly IContactRepository _contactRepository = contactRepository;

        /// <summary>
        /// Handles the contact list search by area code.
        /// </summary>
        /// <param name="request">Data required to search the contacts.</param>
        /// <param name="cancellationToken">Token to cancel the contacts search process.</param>
        /// <returns>A list of contacts that have the same area code used in the search.</returns>
        /// <exception cref="NotFoundException">No contacts were found for the specified filter.</exception>
        public async Task<DefaultOutput<IEnumerable<FindContactByAreaCodeViewModel>>> Handle(FindContactInput request, CancellationToken cancellationToken)
        {
            IEnumerable<ContactEntity> contacts = await _contactRepository.GetContactsByAreaCodeValueAsync(request.AreaCodeValue);
            NotFoundException.ThrowWhenNullOrEmptyList(contacts, "No contacts found for the area code provided");
            return new DefaultOutput<IEnumerable<FindContactByAreaCodeViewModel>>(true, "The contacts were found successfully.", contacts.AsFindContactByAreaCodeViewModel());
        }
    }
}