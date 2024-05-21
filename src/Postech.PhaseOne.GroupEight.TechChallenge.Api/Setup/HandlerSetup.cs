using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ViewModels;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Api.Setup
{
    [ExcludeFromCodeCoverage]
    public static class HandlerSetup
    {
        public static void AddDependencyHandler(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);
            services.AddScoped<IRequestHandler<AddContactInput, DefaultOutput<AddNewContactViewModel>>, AddNewContactHandler>();
            services.AddScoped<IRequestHandler<UpdateContactInput, DefaultOutput<UpdateContactViewModel>>, UpdateContactHandler>();
            services.AddScoped<IRequestHandler<DeleteContactInput, DefaultOutput>, DeleteContactHandler>();
            services.AddScoped<IRequestHandler<FindContactInput, DefaultOutput<IEnumerable<FindContactByAreaCodeViewModel>>>, FindContactByAreaCodeHandler>();
        }
    }
}