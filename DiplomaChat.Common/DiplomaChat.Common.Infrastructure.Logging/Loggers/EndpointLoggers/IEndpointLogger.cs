namespace DiplomaChat.Common.Infrastructure.Logging.Loggers.EndpointLoggers;

public interface IEndpointLogger : ILevelLogger
{
    IEndpointLogger AddRequestBody(string requestBody);
    IEndpointLogger AddResponseBody(string responseBody);
    IEndpointLogger AddUserId(Guid? userId);
    IEndpointLogger AddRequestMethod(string requestMethod);
    IEndpointLogger AddRequestPath(string requestPath);
    IEndpointLogger AddStatusCode(int statusCode);
    IEndpointLogger AddElapsed(int elapsed);
}