using StackExchange.Profiling;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Api.Setup
{
    [ExcludeFromCodeCoverage]
    public static class MiniProfilerSetup
    {
        public static void AddMiniProfiler(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);
            services
                .AddMiniProfiler(options =>
                {
                    options.RouteBasePath = "/contact-management-profiler";
                    options.IgnoredPaths.Add("/swagger");
                    options.ShouldProfile = request => request.Path.Value.Contains("/contacts");
                    options.TrackConnectionOpenClose = true;
                    options.ColorScheme = ColorScheme.Dark;
                    options.EnableMvcFilterProfiling = false;
                    options.EnableMvcViewProfiling = false;
                })
                .AddEntityFramework();
        }
    }
}