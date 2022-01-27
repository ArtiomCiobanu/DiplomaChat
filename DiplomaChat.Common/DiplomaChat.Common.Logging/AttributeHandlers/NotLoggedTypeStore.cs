using System;
using System.Collections.Generic;
using System.Linq;

namespace DiplomaChat.Common.Logging.AttributeHandlers
{
    public class NotLoggedTypeStore : INotLoggedTypeStore
    {
        public Type[] NotLoggedRequestTypes { get; }
        public Type[] NotLoggedResponseTypes { get; }

        public NotLoggedTypeStore(
            IEnumerable<Type> notLoggedRequestTypes,
            IEnumerable<Type> notLoggedResponseTypes)
        {
            NotLoggedRequestTypes = notLoggedRequestTypes.ToArray();
            NotLoggedResponseTypes = notLoggedResponseTypes.ToArray();
        }
    }
}
