using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.IRepository;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

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
            ContactNameValueObject contactName = new ContactNameValueObject(request.Name, request.LastName);
            ContactEmailValueObject contactEmail = new ContactEmailValueObject(request.Email);
            ContactPhoneValueObject contactPhone = new ContactPhoneValueObject(request.Phone, AreaCodeValueObject.Create(request.AreaCode));
            var entity = new ContactEntity(contactName, contactEmail, contactPhone);

            DomainException.ThrowWhen(request.Name == "", "Nome é obrigatório");

            await _contactRepository.InsertAsync(entity);
            return new DefaultOutput(true, "Contato inserido com sucesso");
        }
    }
}