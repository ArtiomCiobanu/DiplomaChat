using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;

namespace DiplomaChat.Common.Infrastructure.Logging.Enrichers
{
    internal class CorrelationIdEnricher : ILogEventEnricher
    {
        private const string CorrelationIdItemName = "CorrelationId";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CorrelationIdEnricher() : this(new HttpContextAccessor())
        {

        }

        public CorrelationIdEnricher(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
            {
                return;
            }

            var requestHeaders = _httpContextAccessor.HttpContext.Request.Headers;

            if (!requestHeaders.ContainsKey(CorrelationIdItemName))
            {
                requestHeaders.Add(CorrelationIdItemName, Guid.NewGuid().ToString());
            }

            var messageEnricher = new MessageEnricher<string>(CorrelationIdItemName, requestHeaders[CorrelationIdItemName]);
            messageEnricher.Enrich(logEvent, propertyFactory);
        }
    }
}
