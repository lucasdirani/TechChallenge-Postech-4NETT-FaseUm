using Prometheus;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Api.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class PrometheusSetup
    {
        public static Summary CreateEndpointRequestDurationSummaryMetric()
        {
            return Metrics.CreateSummary("contact_management_endpoint_duration_seconds", "Duration of HTTP requests in seconds",
                new SummaryConfiguration
                {
                    Objectives =
                    [
                        new QuantileEpsilonPair(0.5, 0.05),
                        new QuantileEpsilonPair(0.9, 0.01),
                        new QuantileEpsilonPair(0.99, 0.001)
                    ],
                    LabelNames = ["method", "endpoint"]
                });
        }

        public static Counter CreateEndpointRequestCounterMetric()
        {
            return Metrics.CreateCounter("contact_management_endpoint_request_total", "Total number of HTTP requests",
                new CounterConfiguration
                {
                    LabelNames = ["method", "endpoint"]
                });
        }
    }
}