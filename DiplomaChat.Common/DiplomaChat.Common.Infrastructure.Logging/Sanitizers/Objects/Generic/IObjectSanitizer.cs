namespace DiplomaChat.Common.Infrastructure.Logging.Sanitizers.Objects.Generic
{
    public interface IObjectSanitizer<T> : IObjectSanitizer
    {
        public string GetSanitizedJson(T value);
    }
}
