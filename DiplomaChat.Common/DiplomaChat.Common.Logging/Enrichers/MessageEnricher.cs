using Serilog.Core;
using Serilog.Events;

namespace DiplomaChat.Common.Logging.Enrichers
{
    public class MessageEnricher<TValue> : IMessageEnricher<TValue>
    {
        private readonly string _propertyName;

        public TValue Message { get; protected set; }

        public MessageEnricher(
            string propertyName,
            TValue message = default)
        {
            _propertyName = propertyName;

            Message = message;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(_propertyName, $"{_propertyName}: {Message} "));
        }
    }
}
