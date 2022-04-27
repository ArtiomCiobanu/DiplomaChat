using Serilog.Core;
using Serilog.Events;

namespace DiplomaChat.Common.Infrastructure.Logging.Enrichers
{
    public class DestructuringEnricher : IMessageEnricher<object>
    {
        private readonly string _propertyName;

        public object Message { get; }

        public DestructuringEnricher(string propertyName, object message = default)
        {
            _propertyName = propertyName;
            Message = message;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(_propertyName, Message, true));
        }
    }
}
