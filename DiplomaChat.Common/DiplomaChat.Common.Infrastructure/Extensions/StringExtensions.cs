using System.ComponentModel;

namespace DiplomaChat.Common.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static TValue? ConvertTo<TValue>(this string value)
        {
            var converter = TypeDescriptor.GetConverter(typeof(TValue));

            var result = (TValue?)converter.ConvertFromString(value);

            return result;
        }
    }
}
