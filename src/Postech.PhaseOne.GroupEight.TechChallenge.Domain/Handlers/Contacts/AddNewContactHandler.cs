using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.IRepository;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts
{
    public class AddNewContactHandler : IRequestHandler<ContactInput, DefaultOutput>
    {
        private readonly IContactRepository _contactRepository;

        public AddNewContactHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<DefaultOutput> Handle(ContactInput request, CancellationToken cancellationToken)
        {

            var entity = new ContactEntity();

            DomainException.ThrowWhen(request.Name == "", "Nome é obrigatproio");

            await _contactRepository.InsertAsync(entity);
            return new DefaultOutput(true, "Contato inserido com sucesso");
        }
    }
}