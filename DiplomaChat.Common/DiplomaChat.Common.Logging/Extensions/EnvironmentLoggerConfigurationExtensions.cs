using DiplomaChat.Common.Logging.Enrichers;
using Serilog;
using Serilog.Configuration;

namespace DiplomaChat.Common.Logging.Extensions
{
    public static class EnvironmentLoggerConfigurationExtensions
    {
        public static LoggerConfiguration WithCorrelationId(this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            return enrichmentConfiguration.With<CorrelationIdEnricher>();
        }
    }
}
