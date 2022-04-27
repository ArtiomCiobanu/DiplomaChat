using DiplomaChat.Common.Infrastructure.Logging.Sanitizers.Endpoint.Request;
using DiplomaChat.Common.Infrastructure.Logging.Sanitizers.Endpoint.Response;
using DiplomaChat.Common.Infrastructure.Responses;

namespace DiplomaChat.Common.Infrastructure.Logging.Sanitizers.Endpoint
{
    public class EndpointSanitizer<TRequest, TResponse> : IEndpointSanitizer<TRequest, TResponse>
        where TResponse : IResponse
    {
        private readonly IRequestSanitizer<TRequest> _requestSanitizer;
        private readonly IResponseSanitizer<TResponse> _responseSanitizer;

        public EndpointSanitizer(
            IRequestSanitizer<TRequest> requestSanitizer,
            IResponseSanitizer<TResponse> responseSanitizer)
        {
            _requestSanitizer = requestSanitizer;
            _responseSanitizer = responseSanitizer;
        }

        public string GetSanitizedRequestJson(TRequest request) => _requestSanitizer.GetSanitizedRequestJson(request);

        public string GetSanitizedResponseJson(TResponse response) => _responseSanitizer.GetSanitizedResponseJson(response);
    }
}
