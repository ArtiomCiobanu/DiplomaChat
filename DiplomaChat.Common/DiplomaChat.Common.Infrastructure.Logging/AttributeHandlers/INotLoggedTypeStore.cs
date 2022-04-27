namespace DiplomaChat.Common.Infrastructure.Logging.AttributeHandlers
{
    public interface INotLoggedTypeStore
    {
        Type[] NotLoggedRequestTypes { get; }
        Type[] NotLoggedResponseTypes { get; }
    }
}
