using DiplomaChat.Common.Infrastructure.Logging.NotLoggedStores.Types;
using DiplomaChat.Common.Infrastructure.Logging.Sanitizers.Objects.Generic;

namespace DiplomaChat.Common.Infrastructure.Logging.Sanitizers.Endpoint.Request
{
    public class RequestSanitizer<TRequest> : IRequestSanitizer<TRequest>
    {
        private readonly Type[] _notLoggedRequestTypes;

        private readonly IObjectSanitizer<TRequest> _objectSanitizer;

        public RequestSanitizer(
            INotLoggedTypeStore ignoreBodyLoggingTypeStore,
            IObjectSanitizer<TRequest> objectSanitizer)
        {
            _notLoggedRequestTypes = ignoreBodyLoggingTypeStore.NotLoggedRequestTypes;

            _objectSanitizer = objectSanitizer;
        }

        public string GetSanitizedRequestJson(TRequest request)
        {
            var result = !_notLoggedRequestTypes.Contains(typeof(TRequest))
                    ? _objectSanitizer.GetSanitizedJson(request)
                    : string.Empty;

            return result;
        }
    }
}
