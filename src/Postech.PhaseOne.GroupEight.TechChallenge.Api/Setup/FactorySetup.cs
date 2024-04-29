using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Factories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Factories.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Api.Setup
{
    [ExcludeFromCodeCoverage]
    public static class FactorySetup
    {
        public static void AddDependencyFactory(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);
            services.AddScoped<IContactPhoneValueObjectFactory, ContactPhoneValueObjectFactory>();
        }
    }
}