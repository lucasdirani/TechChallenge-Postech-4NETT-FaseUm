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

            AreaCodeValueObject areaCodeValueObject = await _contactRepository.GetAreaCodeByValueAsync(request.AreaCode);
            if (areaCodeValueObject == null)
                areaCodeValueObject = AreaCodeValueObject.Create(request.AreaCode);

            ContactPhoneValueObject contactPhone = new(request.Phone, areaCodeValueObject);

            var existstContactPhone = await _contactRepository.
                            GetContactPhoneByNumberAndAreaCodeValueAsync(request.Phone, request.AreaCode);

            DomainException.ThrowWhen(existstContactPhone != null, "Esse telefone já foi cadastrado na lista de contatos.");


            ContactEntity entity = new(contactName, contactEmail, contactPhone);
                        
            await _contactRepository.InsertAsync(entity);
            await _contactRepository.SaveChangesAsync();
            return new DefaultOutput(true, "Contato inserido com sucesso", new { entity });
        }
    }
}