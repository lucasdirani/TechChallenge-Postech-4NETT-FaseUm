using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Checkers;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Checkers.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Api.Setup
{
    [ExcludeFromCodeCoverage]
    public static class CheckerSetup
    {
        public static void AddDependencyChecker(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);
            services.AddKeyedScoped<IRegisteredContactChecker, AddNewContactChecker>(nameof(AddNewContactChecker));
            services.AddKeyedScoped<IRegisteredContactChecker, UpdateContactChecker>(nameof(UpdateContactChecker));
        }
    }
}