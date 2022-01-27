using System;

namespace DiplomaChat.Common.Logging.NotLoggedStores.Types
{
    public interface INotLoggedTypeStore
    {
        Type[] NotLoggedRequestTypes { get; }
        Type[] NotLoggedResponseTypes { get; }
    }
}
