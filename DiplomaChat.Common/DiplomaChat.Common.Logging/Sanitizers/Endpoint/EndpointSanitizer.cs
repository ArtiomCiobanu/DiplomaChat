using DiplomaChat.Common.Infrastructure.Responses;
using DiplomaChat.Common.Logging.Sanitizers.Endpoint.Request;
using DiplomaChat.Common.Logging.Sanitizers.Endpoint.Response;

namespace DiplomaChat.Common.Logging.Sanitizers.Endpoint
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
