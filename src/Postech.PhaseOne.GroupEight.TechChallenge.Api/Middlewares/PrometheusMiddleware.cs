using Prometheus;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Api.Middlewares
{
    [ExcludeFromCodeCoverage]
    internal static class PrometheusMiddleware
    {
        public static IApplicationBuilder UsePrometheusMiddleware(
            this WebApplication app,
            Counter endpointRequestCounterMetric,
            Summary endpointRequestDurationMetric)
        {
            return app.Use(async (context, next) =>
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                endpointRequestCounterMetric.WithLabels(context.Request.Method, context.Request.Path).Inc();
                await next();
                stopwatch.Stop();
                endpointRequestDurationMetric.WithLabels(context.Request.Method, context.Request.Path).Observe(stopwatch.Elapsed.TotalSeconds);
            });
        }
    }
}