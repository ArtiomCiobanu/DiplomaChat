using DiplomaChat.Common.Infrastructure.Logging.Enrichers;
using Serilog;
using Serilog.Configuration;

namespace DiplomaChat.Common.Infrastructure.Logging.Extensions
{
    public static class EnvironmentLoggerConfigurationExtensions
    {
        public static LoggerConfiguration WithMachineName(this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            return enrichmentConfiguration.With<MachineNameEnricher>();
        }

        public static LoggerConfiguration WithCorrelationId(this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            return enrichmentConfiguration.With<CorrelationIdEnricher>();
        }
    }
}
