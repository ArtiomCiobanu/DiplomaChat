using System.Text.RegularExpressions;

namespace DiplomaChat.Common.Logging.Enrichers
{
    public class RegexEnricher : MessageEnricher<string>
    {
        public RegexEnricher(
            string propertyName,
            string message,
            string pattern = @"[\s]+",
            string replacement = " ") : base(propertyName, message)
        {
            ApplyRegexToMessage(pattern, replacement);
        }

        protected void ApplyRegexToMessage(string pattern, string replacement)
        {
            Message = new Regex(pattern).Replace(Message, replacement);
        }
    }
}
