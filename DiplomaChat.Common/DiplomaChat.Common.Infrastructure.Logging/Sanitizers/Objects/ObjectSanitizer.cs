using DiplomaChat.Common.Infrastructure.Logging.NotLoggedStores.Properties;
using Newtonsoft.Json.Linq;

namespace DiplomaChat.Common.Infrastructure.Logging.Sanitizers.Objects
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
            return value == null
                ? string.Empty
                : GetSanitizedJson(value, value.GetType());
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
