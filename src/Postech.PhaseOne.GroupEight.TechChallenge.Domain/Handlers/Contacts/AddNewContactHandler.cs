using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts
{
    public class AddNewContactHandler(IContactRepository contactRepository) : IRequestHandler<ContactInput, DefaultOutput>
    {
        private readonly IContactRepository _contactRepository = contactRepository;

        public async Task<DefaultOutput> Handle(ContactInput request, CancellationToken cancellationToken)
        {
            ContactNameValueObject contactName = new(request.Name, request.LastName);
            ContactEmailValueObject contactEmail = new(request.Email);
            ContactPhoneValueObject contactPhone = new(request.Phone, AreaCodeValueObject.Create(request.AreaCode));
            ContactEntity entity = new(contactName, contactEmail, contactPhone);
            DomainException.ThrowWhen(request.Name == "", "Nome é obrigatório");
            await _contactRepository.InsertAsync(entity);
            return new DefaultOutput(true, "Contato inserido com sucesso");
        }
    }
}