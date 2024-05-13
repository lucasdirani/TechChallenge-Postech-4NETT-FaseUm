﻿using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Factories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Factories.Interfaces;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts
{
    /// <summary>
    /// Inclui um novo contato na agenda
    /// </summary>
    /// <param name="contactRepository">Repositorio do Contato</param>
    /// <param name="contactPhoneValueObjectFactory">Fabrica do telefone</param>
    public class AddNewContactHandler(IContactRepository contactRepository, IContactPhoneValueObjectFactory contactPhoneValueObjectFactory) : IRequestHandler<ContactInput, DefaultOutput>
    {
        private readonly IContactRepository _contactRepository = contactRepository;
        private readonly IContactPhoneValueObjectFactory _contactPhoneValueObjectFactory = contactPhoneValueObjectFactory;

        /// <summary>
        /// Executa as regras de cada campo e incluí o contato na agenda do usuário.
        /// </summary>
        /// <param name="request">Payload com os dados de contato</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<DefaultOutput> Handle(ContactInput request, CancellationToken cancellationToken)
        {
            
            ContactNameValueObject contactName = new(request.Name, request.LastName);
            ContactEmailValueObject contactEmail = new(request.Email);

            var contactPhone = await _contactPhoneValueObjectFactory.CreateAsync(request.Phone, request.AreaCode);
            ContactEntity entity = new(contactName, contactEmail, contactPhone);

            var existsContato = await _contactRepository.ExistsAsync(x => x.ContactEmail.Equals(request.Email)
                && x.ContactName.Equals(request.Name)
                && x.ContactPhone.Number.Equals(request.Phone)
                && x.ContactPhone.AreaCode.Equals(request.AreaCode)) ;

            DomainException.ThrowWhen(existsContato, "Contato já cadastrado.");

            await _contactRepository.InsertAsync(entity);
            await _contactRepository.SaveChangesAsync();
            return new DefaultOutput(true, "Contato inserido com sucesso.", new { entity });
        }
    }
}