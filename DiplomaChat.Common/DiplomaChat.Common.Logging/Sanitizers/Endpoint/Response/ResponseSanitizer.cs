using System;
using System.Linq;
using DiplomaChat.Common.Infrastructure.Responses;
using DiplomaChat.Common.Logging.NotLoggedStores.Types;
using DiplomaChat.Common.Logging.Sanitizers.Objects.Generic;

namespace DiplomaChat.Common.Logging.Sanitizers.Endpoint.Response
{
    public class ResponseSanitizer<TResponse> : IResponseSanitizer<TResponse>
        where TResponse : IResponse
    {
        private readonly Type[] _notLoggedResponseTypes;

        private readonly IObjectSanitizer<TResponse> _objectSanitizer;

        public ResponseSanitizer(
            INotLoggedTypeStore ignoreBodyLoggingTypeStore,
            IObjectSanitizer<TResponse> objectSanitizer)
        {
            _notLoggedResponseTypes = ignoreBodyLoggingTypeStore.NotLoggedResponseTypes;

            _objectSanitizer = objectSanitizer;
        }

        public string GetSanitizedResponseJson(TResponse response)
        {
            var responseType = typeof(TResponse);
            var responseResultType = responseType.IsGenericType
                ? responseType.GenericTypeArguments.First()
                : responseType;

            var result = !_notLoggedResponseTypes.Contains(responseResultType)
                ? _objectSanitizer.GetSanitizedJson(response.Result)
                : string.Empty;

            return result;
        }
    }
}
