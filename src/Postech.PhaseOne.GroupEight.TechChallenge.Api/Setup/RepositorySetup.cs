using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.IRepository;
using Postech.PhaseOne.GroupEight.TechChallenge.Infra.Repositories;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Api.Setup
{
    public static class RepositorySetup
    {
        public static void AddDependencyRepository(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<IContactRepository, ContactRepository>();

        }
    }
}
