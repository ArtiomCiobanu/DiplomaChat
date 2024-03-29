﻿using DiplomaChat.Common.Infrastructure.Logging.NotLoggedStores.Properties;

namespace DiplomaChat.Common.Infrastructure.Logging.Sanitizers.Objects.Generic
{
    public class ObjectSanitizer<T> : ObjectSanitizer, IObjectSanitizer<T>
    {
        public ObjectSanitizer(INotLoggedPropertyStore notLoggedPropertyStore) : base(notLoggedPropertyStore)
        {
        }

        public string GetSanitizedJson(T value) => GetSanitizedJson(value, typeof(T));
    }
}
