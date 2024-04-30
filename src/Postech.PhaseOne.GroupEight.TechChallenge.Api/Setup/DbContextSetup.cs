using Microsoft.EntityFrameworkCore;
using Postech.PhaseOne.GroupEight.TechChallenge.Infra.Data.Contexts;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Api.Setup
{
    [ExcludeFromCodeCoverage]
    public static class DbContextSetup
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configuration);
            services.AddDbContext<ContactManagementDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Scoped
            );
        }
    }
}