using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Handlers.Contacts;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Api.Setup
{
    public static class HandlerSetup
    {
        public static void AddDependencyHandler(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<IRequestHandler<ContactInput, DefaultOutput>, AddNewContactHandler>();

        }
    }
}
