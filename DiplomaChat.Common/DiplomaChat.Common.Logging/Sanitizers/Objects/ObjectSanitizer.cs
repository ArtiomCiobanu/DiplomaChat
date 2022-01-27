﻿using System;
using System.Linq;
using DiplomaChat.Common.Logging.NotLoggedStores.Properties;
using Newtonsoft.Json.Linq;

namespace DiplomaChat.Common.Logging.Sanitizers.Objects
{
    public class ObjectSanitizer : IObjectSanitizer
    {
        private readonly NotLoggedPropertyInfo[] _notLoggedProperties;

        public ObjectSanitizer(INotLoggedPropertyStore notLoggedPropertyStore)
        {
            _notLoggedProperties = notLoggedPropertyStore.NotLoggedProperties;
        }

        public string GetSanitizedJson(object value)
        {
            var valueType = value.GetType();

            return GetSanitizedJson(value, valueType);
        }

        public string GetSanitizedJson(object value, Type valueType)
        {
            var jsonObject = JObject.FromObject(value);

            var existingPropertyNames = _notLoggedProperties
                .Where(p => valueType == p.DeclaringType
                         && jsonObject[p.Name] != null)
                .Select(p => p.Name)
                .ToArray();

            foreach (var propertyName in existingPropertyNames)
            {
                jsonObject.Remove(propertyName);
            }

            return jsonObject.ToString();
        }
    }
}
