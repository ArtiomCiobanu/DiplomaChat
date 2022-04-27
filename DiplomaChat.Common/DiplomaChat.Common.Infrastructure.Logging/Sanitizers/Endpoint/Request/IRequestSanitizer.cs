namespace DiplomaChat.Common.Infrastructure.Logging.Sanitizers.Endpoint.Request
{
    public interface IRequestSanitizer<TRequest>
    {
        string GetSanitizedRequestJson(TRequest request);
    }
}
