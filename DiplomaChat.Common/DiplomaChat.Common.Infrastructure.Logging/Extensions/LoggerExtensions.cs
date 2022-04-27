using DiplomaChat.Common.Infrastructure.Logging.Enrichers;
using Serilog;

namespace DiplomaChat.Common.Infrastructure.Logging.Extensions
{
    public static class LoggerExtensions
    {
        public static ILogger EnrichIfNotEmpty(
            this ILogger logger,
            IMessageEnricher<string> logEventEnricher)
        {
            return string.IsNullOrEmpty(logEventEnricher.Message)
                ? logger
                : logger.ForContext(logEventEnricher);
        }

        public static ILogger EnrichIfNotWhiteSpace(
            this ILogger logger,
            IMessageEnricher<string> logEventEnricher)
        {
            return string.IsNullOrWhiteSpace(logEventEnricher.Message)
                ? logger
                : logger.ForContext(logEventEnricher);
        }

        public static ILogger EnrichIfNotDefault<TValue>(
            this ILogger logger,
            IMessageEnricher<TValue?> logEventEnricher)
            where TValue : struct
        {
            return !logEventEnricher.Message.HasValue || logEventEnricher.Message.Value.Equals(default(TValue))
                ? logger
                : logger.ForContext(logEventEnricher);
        }

        public static ILogger EnrichIfHasValue<TValue>(
            this ILogger logger,
            IMessageEnricher<TValue?> logEventEnricher)
            where TValue : struct
        {
            return !logEventEnricher.Message.HasValue
                ? logger
                : logger.ForContext(logEventEnricher);
        }

        public static ILogger EnrichIfNotNull<TValue>(
            this ILogger logger,
            IMessageEnricher<TValue> logEventEnricher)
            where TValue : class
        {
            return logEventEnricher.Message == null
                ? logger
                : logger.ForContext(logEventEnricher);
        }
    }
}
