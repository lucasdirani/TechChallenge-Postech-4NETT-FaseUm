using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Api.Setup
{
    [ExcludeFromCodeCoverage]
    public static class MediatRSetup
    {
        public static void AddMediatR(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });
        }
    }
}