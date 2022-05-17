using DiplomaChat.Common.Infrastructure.Enums;
using DiplomaChat.Common.Infrastructure.Logging.Accessors.Endpoint;
using DiplomaChat.Common.Infrastructure.Logging.Loggers.EndpointLoggers;
using DiplomaChat.Common.Infrastructure.Logging.Sanitizers.Endpoint;
using DiplomaChat.Common.Infrastructure.Responses;
using MediatR;
using System.Diagnostics;

namespace DiplomaChat.Common.Infrastructure.Logging.RequestPipelines
{
    public class EndpointLoggingPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IResponse
    {
        private readonly IEndpointLogger _endpointLogger;
        private readonly IEndpointSanitizer<TRequest, TResponse> _endpointSanitizer;
        private readonly IEndpointInformationAccessor _endpointInformationAccessor;

        public EndpointLoggingPipeline(
            IEndpointInformationAccessor endpointInformationAccessor,
            IEndpointLogger endpointLogger,
            IEndpointSanitizer<TRequest, TResponse> endpointSanitizer)
        {
            _endpointInformationAccessor = endpointInformationAccessor;
            _endpointLogger = endpointLogger;
            _endpointSanitizer = endpointSanitizer;
        }

        public async Task<TResponse> Handle(
                TRequest request,
                CancellationToken cancellationToken,
                RequestHandlerDelegate<TResponse> next)
        {
            var startTimestamp = Stopwatch.GetTimestamp();

            var requestBody = _endpointSanitizer.GetSanitizedRequestJson(request);
            _endpointLogger
                .AddRequestBody(requestBody)
                .AddRequestMethod(_endpointInformationAccessor.Method)
                .AddRequestPath(_endpointInformationAccessor.Path)
                .AddUserId(_endpointInformationAccessor.UserId);

            try
            {
                TResponse response = await next();

                if (!cancellationToken.IsCancellationRequested)
                {
                    var elapsedMilliseconds = GetElapsedMilliseconds(startTimestamp, Stopwatch.GetTimestamp());

                    var responseBody = _endpointSanitizer.GetSanitizedResponseJson(response);
                    _endpointLogger
                        .AddResponseBody(responseBody)
                        .AddStatusCode((int)response.Status)
                        .AddElapsed(elapsedMilliseconds);

                    _endpointLogger.Warning();
                }

                return response;
            }
            catch
            {
                _endpointLogger.AddStatusCode((int)ResponseStatus.InternalServerError)
                    .Warning();

                throw;
            }
        }

        private int GetElapsedMilliseconds(long start, long stop)
        {
            var result = (stop - start) * 1000 / (double) Stopwatch.Frequency;

            return (int) result;
        }
    }
}