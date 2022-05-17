using Serilog.Core;
using Serilog.Events;

namespace DiplomaChat.Common.Infrastructure.Logging.Enrichers
{
    public class MachineNameEnricher : ILogEventEnricher
    {
        private const string MachineNameItemName = "MachineName";

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var machineName = Environment.MachineName;

            var messageEnricher = new MessageEnricher<string>(MachineNameItemName, machineName);
            messageEnricher.Enrich(logEvent, propertyFactory);
        }
    }
}
