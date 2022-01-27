using System;

namespace DiplomaChat.Common.Logging.AttributeHandlers
{
    public interface INotLoggedTypeStore
    {
        Type[] NotLoggedRequestTypes { get; }
        Type[] NotLoggedResponseTypes { get; }
    }
}
