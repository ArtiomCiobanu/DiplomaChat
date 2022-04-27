namespace DiplomaChat.Common.Infrastructure.Logging.NotLoggedStores.Types
{
    public interface INotLoggedTypeStore
    {
        Type[] NotLoggedRequestTypes { get; }
        Type[] NotLoggedResponseTypes { get; }
    }
}
