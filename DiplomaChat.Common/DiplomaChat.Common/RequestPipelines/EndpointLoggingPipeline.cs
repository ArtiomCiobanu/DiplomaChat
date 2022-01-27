using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using DiplomaChat.Common.Accessors.Endpoint;
using DiplomaChat.Common.Infrastructure.Responses;
using DiplomaChat.Common.Logging.Entries;
using DiplomaChat.Common.Logging.EntryLoggers;
using DiplomaChat.Common.Logging.Sanitizers.Endpoint;
using MediatR;

namespace DiplomaChat.Common.RequestPipelines
{
    public class EndpointLoggingPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IResponse
    {
        private readonly IEntryLogger<EndpointLogEntry> _entryLogger;
        private readonly IEndpointSanitizer<TRequest, TResponse> _endpointSanitizer;
        private readonly IEndpointInformationAccessor _endpointInformationAccessor;

        public EndpointLoggingPipeline(
            IEndpointInformationAccessor endpointInformationAccessor,
            IEntryLogger<EndpointLogEntry> entryLogger,
            IEndpointSanitizer<TRequest, TResponse> endpointSanitizer)
        {
            _entryLogger = entryLogger;

            _endpointInformationAccessor = endpointInformationAccessor;

            _endpointSanitizer = endpointSanitizer;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var startTimestamp = Stopwatch.GetTimestamp();

            var response = await next();

            if (!cancellationToken.IsCancellationRequested)
            {
                var requestBody = _endpointSanitizer.GetSanitizedRequestJson(request);
                var responseBody = _endpointSanitizer.GetSanitizedResponseJson(response);

                var logEntry = new EndpointLogEntry
                {
                    RequestBody = requestBody,
                    ResponseBody = responseBody,

                    Method = _endpointInformationAccessor.Method,
                    Path = _endpointInformationAccessor.Path,

                    StatusCode = (int)response.Status,

                    Elapsed = GetElapsedMilliseconds(startTimestamp, Stopwatch.GetTimestamp()),

                    AccountId = _endpointInformationAccessor.AccountId
                };
                _entryLogger.LogEntry(logEntry);
            }

            return response;
        }

        private int GetElapsedMilliseconds(long start, long stop)
        {
            var result = (stop - start) * 1000 / (double)Stopwatch.Frequency;

            return (int)result;
        }
    }
}