using Serilog.Core;

namespace DiplomaChat.Common.Infrastructure.Logging.Enrichers
{
    public interface IMessageEnricher<TValue> : ILogEventEnricher
    {
        public TValue Message { get; }
    }
}
