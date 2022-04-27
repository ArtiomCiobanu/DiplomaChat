
using DiplomaChat.Common.Infrastructure.Responses;

namespace DiplomaChat.Common.Infrastructure.Logging.Sanitizers.Endpoint.Response
{
    public interface IResponseSanitizer<in TResponse>
        where TResponse : IResponse
    {
        string GetSanitizedResponseJson(TResponse response);
    }
}
