using Serilog.Core;

namespace DiplomaChat.Common.Logging.Enrichers
{
    public interface IMessageEnricher<TValue> : ILogEventEnricher
    {
        public TValue Message { get; }
    }
}
