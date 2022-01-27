namespace DiplomaChat.Common.Logging.NotLoggedStores.Properties
{
    public class NotLoggedPropertyStore : INotLoggedPropertyStore
    {
        public NotLoggedPropertyInfo[] NotLoggedProperties { get; }

        public NotLoggedPropertyStore(NotLoggedPropertyInfo[] notLoggedPropertyNames)
        {
            NotLoggedProperties = notLoggedPropertyNames;
        }
    }
}
