using DiplomaChat.Common.Infrastructure.Logging.Enrichers;
using DiplomaChat.Common.Infrastructure.Logging.Extensions;
using Serilog;

namespace DiplomaChat.Common.Infrastructure.Logging.Loggers.EndpointLoggers;

public class EndpointLogger : IEndpointLogger
{
    private ILogger _logger;

    public EndpointLogger(ILogger logger)
    {
        _logger = logger;
    }

    public IEndpointLogger AddRequestBody(string requestBody)
    {
        _logger = _logger.EnrichIfNotEmpty(new RegexEnricher("RequestBody", requestBody));

        return this;
    }

    public IEndpointLogger AddResponseBody(string responseBody)
    {
        _logger = _logger.EnrichIfNotEmpty(new RegexEnricher("ResponseBody", responseBody));

        return this;
    }

    public IEndpointLogger AddUserId(Guid? userId)
    {
        _logger = _logger.EnrichIfHasValue(new MessageEnricher<Guid?>("UserId", userId));

        return this;
    }

    public IEndpointLogger AddRequestMethod(string requestMethod)
    {
        _logger = _logger.ForContext(new MessageEnricher<string>("RequestMethod", requestMethod));

        return this;
    }

    public IEndpointLogger AddRequestPath(string requestPath)
    {
        _logger = _logger.ForContext(new MessageEnricher<string>("RequestPath", requestPath));

        return this;
    }

    public IEndpointLogger AddStatusCode(int statusCode)
    {
        _logger = _logger.ForContext(new MessageEnricher<int>("StatusCode", statusCode));

        return this;
    }

    public IEndpointLogger AddElapsed(int elapsed)
    {
        _logger = _logger.ForContext(new MessageEnricher<double>("Elapsed", elapsed));

        return this;
    }

    public void Information(string message = "")
    {
        _logger.Information(message);
    }

    public void Warning(string message = "")
    {
        _logger.Warning(message);
    }

    public void Error(string message = "")
    {
        _logger.Error(message);
    }

    public void Fatal(string message = "")
    {
        _logger.Fatal(message);
    }
}