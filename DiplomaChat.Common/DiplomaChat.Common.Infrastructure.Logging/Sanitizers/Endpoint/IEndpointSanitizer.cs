﻿namespace DiplomaChat.Common.Infrastructure.Logging.Sanitizers.Endpoint
{
    public interface IEndpointSanitizer<in TRequest, in TResponse>
    {
        string GetSanitizedRequestJson(TRequest request);
        string GetSanitizedResponseJson(TResponse response);
    }
}
