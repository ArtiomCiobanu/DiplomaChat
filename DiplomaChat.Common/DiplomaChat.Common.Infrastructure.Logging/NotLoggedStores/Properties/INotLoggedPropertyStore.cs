namespace DiplomaChat.Common.Infrastructure.Logging.NotLoggedStores.Properties
{
    public interface INotLoggedPropertyStore
    {
        NotLoggedPropertyInfo[] NotLoggedProperties { get; }
    }
}
