using DiplomaChat.Common.Infrastructure.Logging.Enrichers;
using DiplomaChat.Common.Infrastructure.Logging.Entries;
using DiplomaChat.Common.Infrastructure.Logging.Extensions;
using Serilog;

namespace DiplomaChat.Common.Infrastructure.Logging.EntryLoggers
{
    public class EndpointEntryLogger : IEntryLogger<EndpointLogEntry>
    {
        public void LogEntry(EndpointLogEntry entry)
        {
            Log.Logger
                .EnrichIfNotEmpty(new RegexEnricher("RequestBody", entry.RequestBody))
                .EnrichIfNotEmpty(new RegexEnricher("ResponseBody", entry.ResponseBody))
                .EnrichIfHasValue(new MessageEnricher<Guid?>("UserId", entry.AccountId))
                .ForContext(new MessageEnricher<string>("RequestMethod", entry.Method))
                .ForContext(new MessageEnricher<string>("RequestPath", entry.Path))
                .ForContext(new MessageEnricher<int>("StatusCode", entry.StatusCode))
                .ForContext(new MessageEnricher<double>("Elapsed", entry.Elapsed))
                .Warning("");

            /*.Warning("{RequestMethod} {RequestPath} Status Code: {StatusCode}",
                 entry.Method, entry.Path, entry.StatusCode);*/
        }
    }
}
